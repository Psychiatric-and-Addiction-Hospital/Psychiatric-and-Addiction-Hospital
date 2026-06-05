using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class GetPatientProfileService : IGetPatientProfile
    {
        private readonly AddIdentityDbContext _context;

        public GetPatientProfileService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PatientProfileResponse>> GetProfileAsync(string userId, CancellationToken ct)
        {
            var profile = await _context.PatientProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId, ct);

            if (profile == null)
                return ResponseFactory.Fail<PatientProfileResponse>("Patient profile not found",
                    new System.Collections.Generic.List<string> { "No profile exists for the given userId." });

            return ResponseFactory.Success(new PatientProfileResponse
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FullName = profile.FullName,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender.ToString(),
                MaritalStatus = profile.MaritalStatus.ToString(),
                Occupation = profile.Occupation,
                Address = profile.Address,
                PhoneNumber = profile.PhoneNumber,
                ImageUrl = profile.ImageUrl,
                Email = profile.User?.Email
            }, "Patient profile retrieved successfully");
        }
    }
}
