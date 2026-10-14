using Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Authentication
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

        public string? Email =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
