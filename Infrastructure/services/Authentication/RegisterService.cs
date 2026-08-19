using Application.Common.Constants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using Application.Handlers.Authentication;
using Domain.Entites;
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
    public class RegisterService : IRegister
    {
        private readonly AddIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterHandler> _logger;
        private readonly IEmailVerificationService _emailVerificationService;

        public RegisterService(AddIdentityDbContext context, UserManager<AppUser> userManager,
            ILogger<RegisterHandler> logger,
            IEmailVerificationService emailVerificationService)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailVerificationService = emailVerificationService;
        }
        public async Task<BaseResponse<RegisterResponse>> RegisterAsync(CreatePatientProfileRequest request, CancellationToken ct)
        {
            _logger.LogInformation("Register process started for {Email}", request.Email);
            if (request.Password != request.ConfirmPassword)
            {
                return ResponseFactory.Fail<RegisterResponse>("Password and ConfirmPassword do not matchPassword and ConfirmPassword do not match");

            }

            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ResponseFactory.Fail<RegisterResponse>("Registration failed", errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Patient);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                var errors = roleResult.Errors.Select(x => x.Description).ToList();

                return ResponseFactory.Fail<RegisterResponse>("Failed to assign patient role.", errors);
            }

            var patientProfile = new PatientProfile
            {
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Gender = user.Gender,
                Address = user.Address ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                UserId = user.Id
            };

            await _context.PatientProfiles.AddAsync(patientProfile, ct);
            await _context.SaveChangesAsync(ct);

            var verificationResult = await _emailVerificationService
                .SendVerificationEmailAsync(user.Id, ct);

            if (!verificationResult.Success)
                return ResponseFactory.Fail<RegisterResponse>
                    ("Registration succeeded, but verification email could not be sent.", verificationResult.Errors);

            var dto = new RegisterResponse
            {
                Message = "Registration successful. " + "Please check your email to verify your account."
            };

            return ResponseFactory.Success(dto);
        }
    }
}
