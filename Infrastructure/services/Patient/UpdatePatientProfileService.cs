using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Patient
{
    public class UpdatePatientProfileService : IUpdatePatientProfile
    {
        private readonly AddIdentityDbContext _context;

        public UpdatePatientProfileService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PatientProfileResponse>> UpdateAsync(UpdatePatientProfileRequest request, CancellationToken ct)
        {
            var profile = await _context.PatientProfiles
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.UserId == request.UserId, ct);

            if (profile == null)
                return ResponseFactory.Fail<PatientProfileResponse>("Patient profile not found",
                    new List<string> { "No profile exists for the given userId." });

            profile.AppUser.FirstName = request.FirstName;
            profile.AppUser.LastName = request.LastName;
            profile.DateOfBirth = request.DateOfBirth;
            profile.AppUser.Gender = request.Gender;
            profile.MaritalStatus = request.MaritalStatus;
            profile.AppUser.Address = request.Address;
            profile.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new PatientProfileResponse
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FullName = $"{ profile.AppUser.FirstName }{profile.AppUser.LastName}",
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.AppUser.Gender.ToString(),
                MaritalStatus = profile.MaritalStatus.ToString(),
                Address = profile.AppUser.Address,
                PhoneNumber = profile.PhoneNumber,
                ImageUrl = profile.AppUser.ImageUrl,
                Email = profile.AppUser?.Email
            }, "Patient profile updated successfully");
        }
    }
}
