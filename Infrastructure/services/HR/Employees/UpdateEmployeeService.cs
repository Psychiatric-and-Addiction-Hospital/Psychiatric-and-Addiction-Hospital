using Application.Common.Responses;
using Application.Common.Interfaces.HR.Employee;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Employees
{
    public class UpdateEmployeeService : IUpdateEmployee
    {
        private readonly AddIdentityDbContext _context;

        public UpdateEmployeeService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<EmployeeResponse>> UpdateEmployee(string UserId, string EmployeeCode, string FirstName, string LastName, string Email, Guid DepartmentId, CancellationToken ct)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == EmployeeCode, ct);
            if (employee == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee not found");

            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == DepartmentId, ct);
            if (!departmentExists)
                return ResponseFactory.Fail<EmployeeResponse>("Department not found", new System.Collections.Generic.List<string> { "The provided departmentId does not match any existing department in the system." });

            var emailConflict = await _context.Employees.AnyAsync(e => e.Email == Email && e.Id != employee.Id, ct);
            if (emailConflict)
                return ResponseFactory.Fail<EmployeeResponse>("Email already in use", new System.Collections.Generic.List<string> { "Another employee with this email already exists." });

            // Update fields
            employee.UserId = UserId;
            employee.FirstName = FirstName;
            employee.LastName = LastName;
            employee.Email = Email;
            employee.DepartmentId = DepartmentId;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(ct);

            var response = new EmployeeResponse
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                IsActive = employee.IsActive
            };

            return ResponseFactory.Success(response, "Employee updated successfully");
        }
    }
}
