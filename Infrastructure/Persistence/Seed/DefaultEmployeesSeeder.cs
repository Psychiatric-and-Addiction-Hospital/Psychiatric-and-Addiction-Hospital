using Application.Common.Constants;
using Domain.Entites;
using Domain.Entites.HR;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Seed
{
    public static class DefaultEmployeesSeeder
    {
        private static readonly Guid DefaultDepartmentId = Guid.Parse("48777980-0ad7-4cec-9ad1-f77f48409d67");
        private static readonly Guid DefaultPositionId = Guid.Parse("1c9e0b9b-d329-4d02-b100-df92b3f1c1a4");
        private static readonly Guid DefaultShiftId = Guid.Parse("c7aadaa0-7ef4-474b-8441-75a4478a98da");

        public static async Task SeedEmployeesAsync(
            UserManager<AppUser> userManager,
            AddIdentityDbContext context)
        {
            await SeedEmployeeForUserAsync(
                userManager, context, "admin@kaha.health",
                $"{EmployeeCodePrefixes.Admin}-0001", "00000000000001");

            await SeedEmployeeForUserAsync(
                userManager, context, "hr@kaha.health",
                $"{EmployeeCodePrefixes.HR}-0001", "00000000000002");
        }

        private static async Task SeedEmployeeForUserAsync(
            UserManager<AppUser> userManager,
            AddIdentityDbContext context,
            string email,
            string employeeCode,
            string nationalId)
        {
            var appUser = await userManager.FindByEmailAsync(email);
            if (appUser is null)
                return; // لازم الـ AppUser يتعمل الأول عن طريق DefaultUsersSeeder

            var alreadyExists = await context.Employees
                .AnyAsync(e => e.AppUserId == appUser.Id);

            if (alreadyExists)
                return;

            var employee = new Employee
            {
                EmployeeCode = employeeCode,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email!,
                PhoneNumber = "00000000000",
                HireDate = DateTime.UtcNow,
                NationalId = nationalId,
                EmploymentStatus = EmploymentStatus.Active,
                DepartmentId = DefaultDepartmentId,
                PositionId = DefaultPositionId,
                ShiftId = DefaultShiftId,
                AppUserId = appUser.Id
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();


        }
    }
}