using Application.Common.Interfaces;
using Infrastructure.services.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dependency
{
    public static class DependencyInjectionServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVerifyOtp, VerifyOtp>();

        }
    }
}
