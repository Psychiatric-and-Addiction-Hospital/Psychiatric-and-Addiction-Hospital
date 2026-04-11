using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Common.Interfaces.HR.Employee;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Employees
{
    public class GetByIDEmployeeService : IGetEmployee
    {
        private readonly AddIdentityDbContext _context;

        public GetByIDEmployeeService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<EmployeeResponse>> GetEmployee(Guid Id, CancellationToken ct)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == Id, ct);

            if (employee == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee not found");

            var response = new EmployeeResponse
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                IsActive = employee.IsActive
            };

            return ResponseFactory.Success(response, "Employee retrieved successfully");
        }
    }
}
