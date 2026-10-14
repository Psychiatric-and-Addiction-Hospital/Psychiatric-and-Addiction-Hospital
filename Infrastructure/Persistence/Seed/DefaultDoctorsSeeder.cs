using Application.Common.Constants;
using Domain.Entites;
using Domain.Entites.DoctorsModule;
using Domain.Entites.HR;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed
{
    public class DefaultDoctorsSeeder
    {
        public static async Task SeedDoctorAsync(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AddIdentityDbContext context)
        {
            // 1. التأكد من وجود الرول
            if (!await roleManager.RoleExistsAsync(Roles.Doctor))
                await roleManager.CreateAsync(new IdentityRole(Roles.Doctor));

            var doctorEmail = "doctor@kaha.health";
            var appUser = await userManager.FindByEmailAsync(doctorEmail);

            // 2. التأكد من وجود المستخدم
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

            // 3. إضافة المستخدم لرول الدكتور
            if (!await userManager.IsInRoleAsync(appUser, Roles.Doctor))
                await userManager.AddToRoleAsync(appUser, Roles.Doctor);

            // 4. التأكد من وجود الموظف (الدكتور)
            var employee = await context.Employees
                .FirstOrDefaultAsync(e => e.AppUserId == appUser.Id);

            if (employee is null)
            {
                // ==========================================
                // الحل: جلب أو إنشاء القسم والوظيفة والوردية
                // ==========================================

                var department = await context.Departments.FirstOrDefaultAsync();
                if (department == null)
                {
                    department = new Department { Name = "Medical Department" };
                    context.Departments.Add(department);
                    await context.SaveChangesAsync();
                }

                var position = await context.Positions.FirstOrDefaultAsync();
                if (position == null)
                {
                    position = new Position { Name = "Doctor", DepartmentId = department.Id };
                    context.Positions.Add(position);
                    await context.SaveChangesAsync();
                }

                var shift = await context.Shifts.FirstOrDefaultAsync();
                if (shift == null)
                {
                    shift = new Shift { Name = "Medical Shift", StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)), EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(16)) };
                    context.Shifts.Add(shift);
                    await context.SaveChangesAsync();
                }

                // ==========================================

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

                    // استخدام الـ Ids الحقيقية بدل الثابتة
                    DepartmentId = department.Id,
                    PositionId = position.Id,
                    ShiftId = shift.Id,

                    AppUserId = appUser.Id
                };

                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }

            // 5. التأكد من وجود البروفايل الخاص بالدكتور
            var doctorProfileExists = await context.Set<DoctorProfile>()
                .AnyAsync(d => d.EmployeeId == employee.Id);

            if (!doctorProfileExists)
            {
                var doctorProfile = new DoctorProfile
                {
                    EmployeeId = employee.Id,
                    Specialization = "Psychiatry",
                    LicenseNumber = "LIC-0001",
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