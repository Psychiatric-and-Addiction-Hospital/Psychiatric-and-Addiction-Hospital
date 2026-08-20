using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.UserId == userId, ct);

            if (profile == null)
                return ResponseFactory.Fail<PatientProfileResponse>("Patient profile not found",
                    new System.Collections.Generic.List<string> { "No profile exists for the given userId." });

            return ResponseFactory.Success(new PatientProfileResponse
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FullName = $"{profile.AppUser.FirstName} {profile.AppUser.LastName}",
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.AppUser.Gender.ToString(),
                MaritalStatus = profile.MaritalStatus.ToString(),
                Address = profile.AppUser.Address,
                PhoneNumber = profile.PhoneNumber,
                ImageUrl = profile.AppUser.ImageUrl,
                Email = profile.AppUser?.Email
            }, "Patient profile retrieved successfully");
        }
    }
}
