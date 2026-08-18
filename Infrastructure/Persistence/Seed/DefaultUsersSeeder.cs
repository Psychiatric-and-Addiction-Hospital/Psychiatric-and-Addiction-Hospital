using Application.Common.Constants;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed
{
    public static class DefaultUsersSeeder
    {
        public static async Task SeedAdminsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            if (!await roleManager.RoleExistsAsync(Roles.HR))
                await roleManager.CreateAsync(new IdentityRole(Roles.HR));

            //seed default admin user
            var adminEmail = "admin@kaha.health";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin is null)
            {
                var admin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Hospital",
                    LastName = "Admin",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "P@$$Word55");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        admin,
                        Roles.Admin);
                }
            }

            //seed default hr user
            var hrEmail = "hr@kaha.health";

            var existingHr = await userManager.FindByEmailAsync(hrEmail);

            if (existingHr is null)
            {
                var hr = new AppUser
                {
                    UserName = hrEmail,
                    Email = hrEmail,
                    FirstName = "Hospital",
                    LastName = "HR",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(hr, "P@$$Word55");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(hr, Roles.HR);

            }
        }
    }
}