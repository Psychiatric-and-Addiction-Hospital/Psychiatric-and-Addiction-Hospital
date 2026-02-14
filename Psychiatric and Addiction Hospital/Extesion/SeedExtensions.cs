using Domain.Entites;
using Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Identity;

namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public static class SeedExtensions
    {
        public static async Task Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultUsersSeeder.SeedAdminsAsync(userManager, roleManager);
                await DefaultRolesSeeder.SeedRolesAsync(roleManager);
            }
        }
    }
}
