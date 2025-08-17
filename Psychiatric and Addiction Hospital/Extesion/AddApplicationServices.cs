using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Interfaces;
using Psychiatric_and_Addiction_Hospital.mapping;

namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AppServices(this IServiceCollection services) 
        {
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IAuthService), typeof(TokenService));
            services.AddScoped(typeof(IEmailService), typeof( EmailService));
            services.AddAutoMapper(typeof(MappingProfile));
            return services; 
        }
    }
}
