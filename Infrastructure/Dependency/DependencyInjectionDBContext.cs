using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Dependency
{
    public static class DependencyInjectionDBContext
    {
        public static IServiceCollection AddInfrastructureDBContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AddIdentityDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
