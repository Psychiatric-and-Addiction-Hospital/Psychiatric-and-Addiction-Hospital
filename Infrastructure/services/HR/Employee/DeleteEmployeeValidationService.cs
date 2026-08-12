using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Infrastructure.services.HR.Employee
{
    public class DeleteEmployeeValidationService : IDeleteEmployeeValidation
    {
        private readonly AddIdentityDbContext _context;

        public DeleteEmployeeValidationService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<employeeEntity>> ValidateAsync(DeleteEmployeeRequest request, CancellationToken ct)
        {
            var employee = await _context.Employees
           .Include(x => x.AppUser)
           .FirstOrDefaultAsync(x => x.Id == request.EmployeeId, ct);

            if (employee == null)
                return ResponseFactory.Fail<employeeEntity>("Employee not found.");

            if (!employee.IsActive)
                return ResponseFactory.Fail<employeeEntity>("Employee is already inactive.");

            if (employee.EmploymentStatus == EmploymentStatus.Terminated)
                return ResponseFactory.Fail<employeeEntity>("Employee is already terminated.");

            return ResponseFactory.Success(employee);
        }
    }
}
