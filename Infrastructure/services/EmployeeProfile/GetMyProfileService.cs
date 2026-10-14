using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.EmployeeProfile;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.EmployeeProfile
{
    public class GetMyProfileService : IGetMyProfile
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        public GetMyProfileService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<EmployeeResponse>> GetAsync(CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<EmployeeResponse>("User is not authenticated.");

            var userId = _currentUser.UserId;

            var employee = await _context
                  .WithRole()
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
            if (employee == null)
                return ResponseFactory.Fail<EmployeeResponse>("Employee profile not found.");


            return ResponseFactory.Success(employee, "Employee profile retrieved successfully."
);
        }
    }
}
