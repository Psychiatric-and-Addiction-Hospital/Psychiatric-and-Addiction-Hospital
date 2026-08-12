using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Employee
{
    public class GetEmployeeByIdService : IGetEmployeeById
    {
        private readonly AddIdentityDbContext _context;
        public GetEmployeeByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<EmployeeResponse>> GetByIdAsync(Guid employeeId, CancellationToken ct)
        {
            var employee = await _context.WithRole().Where(x => x.Employee.Id == employeeId && x.Employee.IsActive)
            .Select(e => new EmployeeResponse
            {
                Id = e.Employee.Id,

                EmployeeCode = e.Employee.EmployeeCode,

                FullName = e.Employee.FullName,

                Email = e.Employee.Email,

                PhoneNumber = e.Employee.PhoneNumber,

                NationalId = e.Employee.NationalId,

                Address = e.Employee.AppUser.Address,

                Gender = e.Employee.AppUser.Gender,

                DateOfBirth = e.Employee.DateOfBirth,

                HireDate = e.Employee.HireDate,

                EmploymentStatus = e.Employee.EmploymentStatus,

                IsActive = e.Employee.IsActive,

                DepartmentName = e.Employee.Department.Name,

                PositionName = e.Employee.Position.Name,

                ShiftName = e.Employee.Shift.Name,

                EmergencyContactName = e.Employee.EmergencyContactName,

                EmergencyContactPhone = e.Employee.EmergencyContactPhone,

                ImageUrl = e.Employee.AppUser.ImageUrl,

                Role = e.Role
            }).FirstOrDefaultAsync(ct);

            if (employee == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee not found.");

            return ResponseFactory.Success(employee, "Employee retrieved successfully.");
        }
    }
}
