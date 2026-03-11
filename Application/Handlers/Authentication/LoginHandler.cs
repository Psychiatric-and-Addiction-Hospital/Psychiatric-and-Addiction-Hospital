

using Application.Commands.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public  class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<AuthResult>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(
            UserManager<AppUser> userManager,
            IJwtGenerator jwtGenerator,
            IRefreshTokenService refreshTokenService,
            ILogger<LoginHandler> logger)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _refreshTokenService = refreshTokenService;
            _logger = logger;
        }
        public async Task<BaseResponse<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login attempt for {email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed: user not found");
                
                return ResponseFactory.Fail<AuthResult>(
                    message: "Invalid password or email",
                    errors: new List<string> { "User with this email does not exist" }
                );
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!checkPassword)
            {
                _logger.LogWarning("Login failed: wrong password");
                return ResponseFactory.Fail<AuthResult>(
                    message: "Invalid password or email",
                    errors: new List<string> { "Incorrect password" }
                );
            }

            var accessToken = await _jwtGenerator.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user);

            var authResult = new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Role = user.RoleType.ToString(),
            };

            return ResponseFactory.Success(authResult, "Login successful");

        }

       
    }
}
