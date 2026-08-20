using Application.Common.Constants;
using Domain.Entites;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Seed
{
    public class DefaultPatientsSeeder
    {
        public static async Task SeedPatientAsync(
           UserManager<AppUser> userManager,
           RoleManager<IdentityRole> roleManager,
           AddIdentityDbContext context)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Patient))
                await roleManager.CreateAsync(new IdentityRole(Roles.Patient));

            var patientEmail = "patient@kaha.health";

            var appUser = await userManager.FindByEmailAsync(patientEmail);

            if (appUser is null)
            {
                appUser = new AppUser
                {
                    UserName = patientEmail,
                    Email = patientEmail,
                    FirstName = "Test",
                    LastName = "Patient",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(appUser, "P@$$Word55");

                if (!result.Succeeded)
                    return;
            }

            if (!await userManager.IsInRoleAsync(appUser, Roles.Patient))
                await userManager.AddToRoleAsync(appUser, Roles.Patient);

            var patientProfileExists = await context.Set<PatientProfile>()
                .AnyAsync(p => p.UserId == appUser.Id);

            if (!patientProfileExists)
            {
                var patientProfile = new PatientProfile
                {
                    UserId = appUser.Id,
                    DateOfBirth = new DateTime(1995, 1, 1), 
                    MaritalStatus = MaritalStatus.Single,   
                    PhoneNumber = "00000000000"
                };

                context.Set<PatientProfile>().Add(patientProfile);
                await context.SaveChangesAsync();
            }
        }
    }
}