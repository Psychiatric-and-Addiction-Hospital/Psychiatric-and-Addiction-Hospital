using Domain.Entites;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed
{
    public static class DefaultUsersSeeder
    {
        public static async Task SeedAdminsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            var adminEmail = "ziadsultan733@gmail.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin is null)
            {
                var admin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Ziad",
                    LastName = "Sultan",
                    RoleType = RoleType.Admin,
                    IsActive = true
                };
                await userManager.CreateAsync(admin, "P@$$Word55");

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
