using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Employees
{
    public class CreateEmployeeService : ICreateEmployee
    {
        private readonly AddIdentityDbContext _context;

        public CreateEmployeeService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<EmployeeResponse>> CreateAsync(
         string userId, string employeeCode, string firstName, string lastName, string email, Guid departmentId, CancellationToken ct)
        {
            var departmentExists = await _context.Departments
                .AnyAsync(d => d.Id == departmentId, ct);

            if (!departmentExists)
            {
                return ResponseFactory.Fail<EmployeeResponse>("Department not found",
                    new List<string> { "The provided departmentId does not match any existing department in the system." });
            }

       
            var emailExists = await _context.Employees
                .AnyAsync(e => e.Email == email, ct);

            if (emailExists)
            {
                return ResponseFactory.Fail<EmployeeResponse>("Email already in use",
                    new List<string> { "An employee with this email already exists." });
            }

        
            var codeExists = await _context.Employees
                .AnyAsync(e => e.EmployeeCode == employeeCode, ct);

            if (codeExists)
            {
                return ResponseFactory.Fail<EmployeeResponse>("Employee code already in use",
                    new List<string> { "The provided employee code is already assigned to another employee." });
            }

         
            var employee = new Employee
            {
                UserId = userId,
                EmployeeCode = employeeCode,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DepartmentId = departmentId,
                IsActive = true 
            };

      
            await _context.Employees.AddAsync(employee, ct);
            await _context.SaveChangesAsync(ct);


            return ResponseFactory.Success(new EmployeeResponse
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                IsActive = employee.IsActive
            }, "Employee created successfully");
        }
    }
}
