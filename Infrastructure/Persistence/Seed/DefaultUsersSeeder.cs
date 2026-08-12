using Application.Common.Constants;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed
{
    public static class DefaultUsersSeeder
    {
        public static async Task SeedAdminsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.HR))
                await roleManager.CreateAsync(new IdentityRole(Roles.HR));

            var adminEmail = "HRZiadSultan@gmail.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin is null)
            {
                var HR = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Ziad",
                    LastName = "Sultan",
                    IsActive = true
                };
                await userManager.CreateAsync(HR, "P@$$Word55");

                await userManager.AddToRoleAsync(HR, Roles.HR);
            }
        }
    }
}
