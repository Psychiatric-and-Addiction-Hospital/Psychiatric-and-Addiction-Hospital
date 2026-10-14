using Application.Common.Interfaces.Authentication;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Infrastructure.services.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        public JwtGenerator(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FirstName", user.FirstName),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["JWT:ValidIssuer"],
                _config["JWT:ValidAudience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

