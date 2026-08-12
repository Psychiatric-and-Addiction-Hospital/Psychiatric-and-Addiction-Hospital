using Application.Commands.Authentication;
using Application.Common.Constants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, BaseResponse<RegisterResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<RegisterHandler> _logger;
        private readonly IEmailVerificationService _emailVerificationService;

        public RegisterHandler(
            UserManager<AppUser> userManager,
            IJwtGenerator jwtGenerator,
            ILogger<RegisterHandler> logger,
            IEmailVerificationService emailVerificationService)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _logger = logger;
            _emailVerificationService = emailVerificationService;
        }
        public async Task<BaseResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                Gender = request.gender,
                PhoneNumber = request.PhoneNumber,
                Address = request.Addres
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

            var verificationResult = await _emailVerificationService
                .SendVerificationEmailAsync(user.Id, cancellationToken);

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

