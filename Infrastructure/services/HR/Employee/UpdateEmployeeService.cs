using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.DTOS.Responses.HR.Employee;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.Employee
{
    public class UpdateEmployeeService : IUpdateEmployee
    {
        private readonly AddIdentityDbContext _context;
        private readonly IEmployeeValidation _validation;
        public UpdateEmployeeService(AddIdentityDbContext context, IEmployeeValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<EmployeeResponse>> UpdateAsync(UpdateEmployeeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<EmployeeResponse>(validation.Message, validation.Errors);

            var employee = validation.Data!;

            #region Update Employee

            employee.FirstName = request.FirstName.Trim();

            employee.LastName = request.LastName.Trim();

            employee.PhoneNumber = request.PhoneNumber.Trim();

            employee.AppUser.Address = request.Address.Trim();

            employee.DepartmentId = request.DepartmentId;

            employee.PositionId = request.PositionId;

            employee.ShiftId = request.ShiftId;

            employee.ManagerId = request.ManagerId;

            employee.EmergencyContactName =
                request.EmergencyContactName?.Trim();

            employee.EmergencyContactPhone =
                request.EmergencyContactPhone?.Trim();

            employee.AppUser.ImageUrl = request.ImageUrl;

            employee.IsActive = request.IsActive;

            #endregion

            #region Update Identity User

            if (employee.AppUser != null)
            {
                employee.AppUser.FirstName = employee.FirstName;

                employee.AppUser.LastName = employee.LastName;

                employee.AppUser.PhoneNumber = employee.PhoneNumber;

                employee.AppUser.Address = employee.AppUser.Address;

                employee.AppUser.IsActive = employee.IsActive;
            }

            #endregion

            await _context.SaveChangesAsync(ct);

            await _context.Entry(employee)
                .Reference(x => x.Department)
                .LoadAsync(ct);

            await _context.Entry(employee)
                .Reference(x => x.Position)
                .LoadAsync(ct);

            await _context.Entry(employee)
                .Reference(x => x.Shift)
                .LoadAsync(ct);

            return ResponseFactory.Success(new EmployeeResponse
            {
                Id = employee.Id,

                EmployeeCode = employee.EmployeeCode,

                FullName = employee.FullName,

                Email = employee.Email,

                PhoneNumber = employee.PhoneNumber,

                DepartmentName = employee.Department.Name,

                PositionName = employee.Position.Name,

                ShiftName = employee.Shift.Name,

                EmploymentStatus = employee.EmploymentStatus,

                IsActive = employee.IsActive,

                HireDate = employee.HireDate,

                ImageUrl = employee.AppUser.ImageUrl
            },
                "Employee updated successfully.");
        }
    }
}
