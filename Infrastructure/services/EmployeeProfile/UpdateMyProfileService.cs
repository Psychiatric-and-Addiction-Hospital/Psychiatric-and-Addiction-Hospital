using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.EmployeeProfile;
using Application.Common.Responses;
using Application.DTOS.Request.EmployeeProfile;
using Application.DTOS.Responses.HR.Employee;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.EmployeeProfile
{
    public class UpdateMyProfileService : IUpdateMyProfile
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        public UpdateMyProfileService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<EmployeeResponse>> UpdateAsync(UpdateMyProfileRequest request, CancellationToken ct)
        {

            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<EmployeeResponse>("User is not authenticated.");


            var employee = await _context.Employees
                .Include(x => x.AppUser)
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.AppUserId == _currentUser.UserId, ct);

            if (employee == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee profile not found.");

            var phoneExists = await _context.Employees
                .AnyAsync(x => x.PhoneNumber == request.PhoneNumber
                && x.Id != employee.Id, ct);

            if (phoneExists)
                return ResponseFactory.Fail<EmployeeResponse>("Phone number already exists.");

            employee.PhoneNumber = request.PhoneNumber.Trim();

            employee.AppUser.Address = request.Address.Trim();

            employee.DateOfBirth = request.DateOfBirth;

            employee.EmergencyContactName = request.EmergencyContactName?.Trim();

            employee.EmergencyContactPhone = request.EmergencyContactPhone?.Trim();

            employee.AppUser.ImageUrl = request.ImageUrl;

            if (employee.AppUser != null)
            {
                employee.AppUser.PhoneNumber = employee.PhoneNumber;
                employee.AppUser.Address = employee.AppUser.Address;
            }

            await _context.SaveChangesAsync(ct);

            var employeeresponse = await _context
                 .WithRole()
                 .AsNoTracking()
                 .Where(x => x.Employee.AppUserId == _currentUser.UserId)
                 .Select(x => new EmployeeResponse
                 {
                     Id = x.Employee.Id,

                     EmployeeCode = x.Employee.EmployeeCode,

                     FullName = x.Employee.FullName,

                     Email = x.Employee.Email,

                     PhoneNumber = x.Employee.PhoneNumber,

                     NationalId = x.Employee.NationalId,

                     Address = x.Employee.AppUser.Address,

                     Gender = x.Employee.AppUser.Gender,

                     DateOfBirth = x.Employee.DateOfBirth,

                     HireDate = x.Employee.HireDate,

                     EmploymentStatus = x.Employee.EmploymentStatus,

                     IsActive = x.Employee.IsActive,

                     DepartmentName = x.Employee.Department.Name,

                     PositionName = x.Employee.Position.Name,

                     ShiftName = x.Employee.Shift.Name,

                     EmergencyContactName =
                         x.Employee.EmergencyContactName,

                     EmergencyContactPhone =
                         x.Employee.EmergencyContactPhone,

                     ImageUrl = x.Employee.AppUser.ImageUrl,

                     Role = x.Role
                 })
                 .FirstOrDefaultAsync(ct);

            if (employeeresponse == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee profile not found.");

            return ResponseFactory.Success(employeeresponse, "Employee profile updated successfully.");

        }
    }
}
