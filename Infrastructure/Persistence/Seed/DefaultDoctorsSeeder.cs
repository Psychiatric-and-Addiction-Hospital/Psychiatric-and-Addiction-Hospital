using Application.Common.Constants;
using Domain.Entites;
using Domain.Entites.DoctorsModule;
using Domain.Entites.HR;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seed
{
    public class DefaultDoctorsSeeder
    {
        private static readonly Guid DoctorDepartmentId = Guid.Parse("48777980-0ad7-4cec-9ad1-f77f48409d67");
        private static readonly Guid DoctorPositionId = Guid.Parse("1c9e0b9b-d329-4d02-b100-df92b3f1c1a4");
        private static readonly Guid DoctorShiftId = Guid.Parse("c7aadaa0-7ef4-474b-8441-75a4478a98da");

        public static async Task SeedDoctorAsync(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AddIdentityDbContext context)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Doctor))
                await roleManager.CreateAsync(new IdentityRole(Roles.Doctor));

            var doctorEmail = "doctor@kaha.health";

            var appUser = await userManager.FindByEmailAsync(doctorEmail);

            if (appUser is null)
            {
                appUser = new AppUser
                {
                    UserName = doctorEmail,
                    Email = doctorEmail,
                    FirstName = "Hospital",
                    LastName = "Doctor",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(appUser, "P@$$Word55");

                if (!result.Succeeded)
                    return;
            }

            if (!await userManager.IsInRoleAsync(appUser, Roles.Doctor))
                await userManager.AddToRoleAsync(appUser, Roles.Doctor);

            var employee = await context.Employees
                .FirstOrDefaultAsync(e => e.AppUserId == appUser.Id);

            if (employee is null)
            {
                employee = new Employee
                {
                    EmployeeCode = $"{EmployeeCodePrefixes.Doctor}-0001",
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Email = appUser.Email!,
                    PhoneNumber = "00000000000",
                    HireDate = DateTime.UtcNow,
                    NationalId = "00000000000003",
                    EmploymentStatus = EmploymentStatus.Active,
                    DepartmentId = DoctorDepartmentId,
                    PositionId = DoctorPositionId,
                    ShiftId = DoctorShiftId,
                    AppUserId = appUser.Id
                };

                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }

            var doctorProfileExists = await context.Set<DoctorProfile>()
                .AnyAsync(d => d.EmployeeId == employee.Id);

            if (!doctorProfileExists)
            {
                var doctorProfile = new DoctorProfile
                {
                    EmployeeId = employee.Id,
                    Specialization = "Psychiatry", // TODO: عدّل حسب التخصص المطلوب
                    LicenseNumber = "LIC-0001",     // TODO: رقم ترخيص حقيقي
                    Qualifications = "MBBCh, MSc Psychiatry",
                    Degree = "MSc",
                    YearsOfExperience = 5
                };

                context.Set<DoctorProfile>().Add(doctorProfile);
                await context.SaveChangesAsync();
            }
        }
    }
}