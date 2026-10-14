using Application.Common.Constants;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed
{
    public static class DefaultRolesSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { Roles.Admin, Roles.HR, Roles.Doctor, Roles.Nurse, Roles.Patient, Roles.Receptionist, Roles.Candidate };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
