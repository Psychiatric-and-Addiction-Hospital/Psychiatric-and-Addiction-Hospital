using Application.Common.Interfaces;
using Domain.Entites;
using Domain.Entites.Authentication;
using FluentResults;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Authentication
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AddIdentityDbContext _context;
        private readonly IEmailService _email;
        private readonly ILogger<PasswordResetService> _logger;

        public PasswordResetService(
            UserManager<AppUser> userManager,
            AddIdentityDbContext context,
            IEmailService email,
            ILogger<PasswordResetService> logger)
        {
            _userManager = userManager;
            _context = context;
            _email = email;
            _logger = logger;
        }
        public async Task<Result<string>> SendForgetPasswordOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result.Fail("Email not found.");

            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            var code = new PasswordResetCode
            {
                UserId = user.Id,
                Code = otp,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10)
            };

            _context.PasswordResetCodes.Add(code);
            await _context.SaveChangesAsync();

            // Send Email
            await _email.SendAsync(user.Email, "Reset Password OTP", $"Your OTP is: {otp}");

            _logger.LogInformation("OTP sent to {email}", user.Email);

            return Result.Ok("OTP sent to email.");
        }
    }
}
    

