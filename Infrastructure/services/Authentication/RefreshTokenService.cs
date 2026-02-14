using Application.Common.Interfaces.Authentication;
using Application.DTOS.Responses;
using Domain.Entites;
using Domain.Entites.Authentication;
using FluentResults;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;


namespace Infrastructure.services.Authentication
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AddIdentityDbContext _context;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IConfiguration _config;

        public RefreshTokenService(
            UserManager<AppUser> userManager,
            AddIdentityDbContext context,
            IJwtGenerator jwtGenerator,
            IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _jwtGenerator = jwtGenerator;
            _config = config;
        }
        public async Task<string> GenerateRefreshTokenAsync(AppUser user)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refresh = new RefreshToken
            {
                Token = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(10),
                IsRevoked = false,
                IsUsed = false
            };
            _context.Set<RefreshToken>().Add(refresh);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<Result<AuthResult>> RefreshAsync(string refreshToken)
        {
            var stored = await _context.Set<RefreshToken>()
           .Include(x => x.User)
           .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (stored is null || stored.IsRevoked || stored.IsUsed || stored.ExpiresAt < DateTime.UtcNow)
                return Result.Fail("Invalid refresh token.");

            stored.IsUsed = true;
            await _context.SaveChangesAsync();

            var accessToken = await _jwtGenerator.GenerateTokenAsync(stored.User);
            var newRefreshToken = await GenerateRefreshTokenAsync(stored.User);

            return Result.Ok(new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}

