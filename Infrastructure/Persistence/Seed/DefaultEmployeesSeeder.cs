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
                return; // لازم الـ AppUser يتعمل الأول

            var alreadyExists = await context.Employees
                .AnyAsync(e => e.AppUserId == appUser.Id);

            if (alreadyExists)
                return;

            // 1. جلب أول قسم متاح من قاعدة البيانات
            var department = await context.Departments.FirstOrDefaultAsync();
            if (department == null)
            {
                department = new Department { Name = "General Department" }; // تأكد من الخصائص المطلوبة في كلاس القسم
                context.Departments.Add(department);
                await context.SaveChangesAsync();
            }

            // 2. جلب أول وظيفة متاحة
            var position = await context.Positions.FirstOrDefaultAsync();
            if (position == null)
            {
                position = new Position { Name = "General Position", DepartmentId = department.Id }; // تأكد من الخصائص المطلوبة
                context.Positions.Add(position);
                await context.SaveChangesAsync();
            }

            // 3. جلب أول وردية متاحة
            var shift = await context.Shifts.FirstOrDefaultAsync();
            if (shift == null)
            {
                shift = new Shift { Name = "Morning Shift", StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)), EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(17)) }; // تأكد من الخصائص المطلوبة
                context.Shifts.Add(shift);
                await context.SaveChangesAsync();
            }

            // 4. إنشاء الموظف بالـ Ids الحقيقية من الداتابيز
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

             
                DepartmentId = department.Id,
                PositionId = position.Id,
                ShiftId = shift.Id,

                AppUserId = appUser.Id
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }
    }
}