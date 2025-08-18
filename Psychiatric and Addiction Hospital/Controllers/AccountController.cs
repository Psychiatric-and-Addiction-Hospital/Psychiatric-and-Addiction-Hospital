using Application.DTO;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psychiatric_and_Addiction_Hospital.Erroes;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{

    public class AccountController : BaseController
    {

        private readonly UserManager<AppUser> _User;
        private readonly SignInManager<AppUser> _signin;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _AuthService;
        public readonly AddIdentityDbContext _context;
        public readonly IEmailService _EmailService;

        private readonly IMapper _mapper;
        public AccountController(
            UserManager<AppUser> User, SignInManager<AppUser> signin, IMapper mapper, IAuthService AuthService, AddIdentityDbContext context, IEmailService EmailService
            )
        {
            _User = User;
            _signin = signin;
            _mapper = mapper;
            _EmailService = EmailService;
            _context = context;
            _AuthService = AuthService;


        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto)
        {
            var checkemail = await _User.FindByEmailAsync(logindto.Email);
            if (checkemail == null) return Unauthorized(new ApiResponse(401));
            var result = await _signin.CheckPasswordSignInAsync(checkemail, logindto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            var Tokens = await _AuthService.CreateTokenWithRefresh(checkemail);
            var userDto = _mapper.Map<UserDto>(checkemail);
            userDto.AccessToken = Tokens.AccessToken;
            userDto.RefreshToken = Tokens.RefreshToken;
            return Ok(userDto);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await _User.FindByEmailAsync(register.Email) != null)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Email is already in use" }

                });
            }
            else
            {
                if (register.Password != register.ConfirmPassword)
                {
                    return BadRequest(new ApiResponse(400, "Password and Confirm Password do not match"));
                }
                var user = _mapper.Map<AppUser>(register);
                var result = await _User.CreateAsync(user, register.Password);

                if (!result.Succeeded) return BadRequest(new ApiResponse(400));
                var Tokens = await _AuthService.CreateTokenWithRefresh(user);
                var userDto = _mapper.Map<UserDto>(user);
                userDto.AccessToken = Tokens.AccessToken;
                userDto.RefreshToken = Tokens.RefreshToken;

                return Ok(userDto);
            }

        }
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenDto>> Refresh(TokenDto tokenDto)
        {
            var user = await _User.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == tokenDto.RefreshToken));

            if (user == null)
                return Unauthorized(new ApiResponse(401, "Invalid refresh token"));

            var oldToken = user.RefreshTokens.FirstOrDefault(t => t.Token == tokenDto.RefreshToken);

            if (oldToken == null || !oldToken.IsActive)
                return Unauthorized(new ApiResponse(401, "Refresh token is not active"));

            oldToken.Revoked = DateTime.UtcNow;
            oldToken.ReplacedByToken = "new-token-pending";

            var tokens = await _AuthService.CreateTokenWithRefresh(user);

            oldToken.ReplacedByToken = tokens.RefreshToken;
            await _User.UpdateAsync(user);

            return Ok(tokens);
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpDto dto)
        {
            var user = await _User.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "Email not found"));

            // توليد OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // حفظ OTP في قاعدة البيانات
            var resetCode = new PasswordResetCode
            {
                Email = dto.Email,
                Code = otp,
                Expiry = DateTime.UtcNow.AddMinutes(10),
                Used = false
            };

            await _context.PasswordResetCodes.AddAsync(resetCode);
            await _context.SaveChangesAsync();

            // إرسال الإيميل
            string subject = "Reset Code";
            string body = $"<p>Your OTP is: <strong>{otp}</strong></p><p>It expires in 10 minutes.</p>";

            await _EmailService.SendOtpAsync(dto.Email, subject, body);

            return Ok(new ApiResponse(200, "OTP sent to your email"));
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
        {
            var resetCode = await _context.PasswordResetCodes
                .Where(c => c.Email == dto.Email && c.Code == dto.Code && !c.Used && c.Expiry > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (resetCode == null)
                return BadRequest(new ApiResponse(400, "Invalid or expired OTP"));

            resetCode.Used = true;
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(200, "OTP verified, you can now reset your password"));
        }

        [HttpPost("reset-password-otp")]
        public async Task<IActionResult> ResetPasswordUsingOtp([FromBody] ResetPasswordOtpDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest(new ApiResponse(400, "Passwords do not match"));

            var user = await _User.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "Invalid Email"));

            
            var resetCode = await _context.PasswordResetCodes
                .FirstOrDefaultAsync(x => x.Email == dto.Email && x.Code == dto.Otp
                );
            

            if (resetCode == null)
                return BadRequest(new ApiResponse(400, "Invalid or expired OTP"));

            // توليد توكن تغيير الباسورد
            var token = await _User.GeneratePasswordResetTokenAsync(user);

            var result = await _User.ResetPasswordAsync(user, token, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "Password reset failed"));

            // تعليم الكود كـ مستخدم
            resetCode.Used = true;
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(200, "Password has been reset successfully"));
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] SendOtpDto dto)
        {
            var user = await _User.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "Email not found"));

            var lastCode = await _context.PasswordResetCodes
                .Where(c => c.Email == dto.Email && !c.Used && c.Expiry > DateTime.UtcNow)
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync();

            if (lastCode != null)
            {
                var timeSinceLastCode = DateTime.UtcNow - lastCode.CreatedAt;
                if (timeSinceLastCode.TotalSeconds < 60)
                {
                    return BadRequest(new ApiResponse(400, $"Please wait {60 - (int)timeSinceLastCode.TotalSeconds} seconds before requesting a new OTP."));
                }
            }

            
            var otp = new Random().Next(100000, 999999).ToString();

            var newCode = new PasswordResetCode
            {
                Email = dto.Email,
                Code = otp,
                CreatedAt = DateTime.UtcNow,
                Expiry = DateTime.UtcNow.AddMinutes(10),
                Used = false
            };

            await _context.PasswordResetCodes.AddAsync(newCode);
            await _context.SaveChangesAsync();

            string subject = "Reset Code";
            string body = $"<p>Your OTP is: <strong>{otp}</strong></p><p>It expires in 10 minutes.</p>";
            await _EmailService.SendOtpAsync(dto.Email, subject, body);

            return Ok(new ApiResponse(200, "OTP sent to your email"));
        }

    }
}
