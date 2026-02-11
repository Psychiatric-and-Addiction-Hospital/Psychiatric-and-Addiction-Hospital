

using Application.Commands.Authentication;
using Application.Common.Interfaces;
using Application.DTOS.Responses;
using Domain.Entites;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResult>>
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
        public async Task<Result<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login attempt for {email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed: user not found");
                return Result.Fail("Invalid credentials.");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!checkPassword)
            {
                _logger.LogWarning("Login failed: wrong password");
                return Result.Fail("Invalid credentials.");
            }

            var accessToken = await _jwtGenerator.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user);

            return Result.Ok(new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

    }
}
