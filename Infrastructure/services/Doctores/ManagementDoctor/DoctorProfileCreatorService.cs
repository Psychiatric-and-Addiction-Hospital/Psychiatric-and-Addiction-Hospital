using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.DTOS.Request.HR.Employee;
using Domain.Entites;
using Domain.Entites.DoctorsModule;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.Doctores.ManagementDoctor
{
    public class DoctorProfileCreatorService : IDoctorProfileCreator
    {
        private readonly AddIdentityDbContext _context;

        public DoctorProfileCreatorService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(AppUser appUser, Employee employee, HireEmployeeRequest request, CancellationToken ct)
        {
            var doctorProfile = new DoctorProfile
            {
                EmployeeId = employee.Id,

                Degree = request.DoctorProfile?.Degree!.Trim(),

                Specialization = request.DoctorProfile?.Specialization!.Trim(),

                LicenseNumber = request.DoctorProfile?.LicenseNumber!.Trim(),

                Qualifications = request.DoctorProfile?.Qualifications!.Trim(),

                YearsOfExperience = request.DoctorProfile.YearsOfExperience
            };

            await _context.DoctorProfiles.AddAsync(doctorProfile, ct);
        }
    }
}
