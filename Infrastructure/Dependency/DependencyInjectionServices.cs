using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores;
using Infrastructure.services.Admin.DoctorManagement;
using Infrastructure.services.Authentication;
using Infrastructure.services.Doctores;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Dependency
{
    public static class DependencyInjectionServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // ---------- Authentication ----------
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVerifyOtp, VerifyOtp>();

            // ---------- Doctores ----------
            services.AddScoped<IDoctoreApplication, DoctoreApplicationService>();
            services.AddScoped<IAdminDoctorManagement, AdminDoctorManagementService>();


        }
    }
}
