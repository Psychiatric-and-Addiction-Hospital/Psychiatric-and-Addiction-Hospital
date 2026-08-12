using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Infrastructure.services.HR.Employee
{
    public class EmployeeValidationService : IEmployeeValidation
    {
        private readonly AddIdentityDbContext _context;
        public EmployeeValidationService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<employeeEntity>> ValidateAsync(UpdateEmployeeRequest request, CancellationToken ct)
        {

            var employee = await _context.Employees
                .Include(x => x.AppUser)
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.Id == request.EmployeeId, ct);

            if (employee == null)
                return ResponseFactory.Fail<employeeEntity>("Employee not found.");

            if (!await _context.Departments.AnyAsync(x => x.Id == request.DepartmentId, ct))
                return ResponseFactory.Fail<employeeEntity>("Department not found.");

            if (!await _context.Positions.AnyAsync(x => x.Id == request.PositionId, ct))
                return ResponseFactory.Fail<employeeEntity>("Position not found.");

            if (!await _context.Shifts.AnyAsync(x => x.Id == request.ShiftId, ct))
                return ResponseFactory.Fail<employeeEntity>("Shift not found.");

            if (request.ManagerId == request.EmployeeId)
                return ResponseFactory.Fail<employeeEntity>("Employee cannot be his own manager.");

            if (request.ManagerId.HasValue)
            {
                var managerExists = await _context.Employees
                    .AnyAsync(x => x.Id == request.ManagerId.Value, ct);

                if (!managerExists)
                    return ResponseFactory.Fail<employeeEntity>("Manager not found.");
            }

            var phoneExists = await _context.Employees
           .AnyAsync(x =>
               x.PhoneNumber == request.PhoneNumber &&
               x.Id != request.EmployeeId,
               ct);

            if (phoneExists)
                return ResponseFactory.Fail<employeeEntity>(
                    "Phone number is already used by another employee.");

            return ResponseFactory.Success(employee);
        }
    }
}
