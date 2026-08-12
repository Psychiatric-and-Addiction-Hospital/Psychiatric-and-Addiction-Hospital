using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Employee
{
    public class RestoreEmployeeValidationService : IRestoreEmployeeValidation
    {
        private readonly AddIdentityDbContext _context;

        public RestoreEmployeeValidationService(
            AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<Domain.Entites.HR.Employee>> ValidateAsync(RestoreEmployeeRequest request, CancellationToken ct)
        {
            var employee = await _context.Employees
                .Include(x => x.AppUser)
                .FirstOrDefaultAsync(
                    x => x.Id == request.EmployeeId,
                    ct);

            if (employee == null)
                return ResponseFactory.Fail<Domain.Entites.HR.Employee>(
                    "Employee not found.");

            if (employee.IsActive)
                return ResponseFactory.Fail<Domain.Entites.HR.Employee>(
                    "Employee is already active.");

            return ResponseFactory.Success(employee);
        }
    }
}