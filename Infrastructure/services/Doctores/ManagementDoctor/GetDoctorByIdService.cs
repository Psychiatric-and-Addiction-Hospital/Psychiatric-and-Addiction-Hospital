using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Doctores.ManagementDoctor
{
    public class GetDoctorByIdService : IGetDoctorById
    {
        private readonly AddIdentityDbContext _context;
        public GetDoctorByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<DoctorProfileResponse>> GetDoctorByIdAsync(Guid Id, CancellationToken ct)
        {
            var doctor = await _context.DoctorProfiles
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .Select(x => new DoctorProfileResponse
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,

                    FullName = x.Employee.FullName,
                    Email = x.Employee.Email,
                    PhoneNumber = x.Employee.PhoneNumber,

                    Specialization = x.Specialization,
                    LicenseNumber = x.LicenseNumber,
                    Degree = x.Degree,
                    YearsOfExperience = x.YearsOfExperience,

                    ImagePath = x.Employee.AppUser.ImageUrl,
                    Gender = x.Employee.AppUser.Gender,

                    DepartmentId = x.Employee.DepartmentId,
                    DepartmentName = x.Employee.Department.Name,

                    PositionId = x.Employee.PositionId,
                    PositionName = x.Employee.Position.Name,

                    IsActive = x.Employee.IsActive
                }).FirstOrDefaultAsync(ct);

            if (doctor == null)
                return ResponseFactory.Fail<DoctorProfileResponse>("Doctor not found.");

            return ResponseFactory.Success(doctor, "Doctor Profile Retrieved Successfully");

        }
    }
}
