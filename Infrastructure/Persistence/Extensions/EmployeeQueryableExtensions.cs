using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    public static class EmployeeQueryableExtensions
    {
        public static IQueryable<EmployeeWithRole> WithRole(this AddIdentityDbContext context)
        {
            return from employee in context.Employees.AsNoTracking()
                   .Include(x => x.Department)
                   .Include(x => x.Position)
                   .Include(x => x.Shift)
                   join user in context.Users on employee.AppUserId equals user.Id
                   join userRole in context.UserRoles on user.Id equals userRole.UserId
                   join role in context.Roles on userRole.RoleId equals role.Id

                   select new EmployeeWithRole
                   {
                       Employee = employee,
                       Role = role.Name!
                   };
        }
    }
}
