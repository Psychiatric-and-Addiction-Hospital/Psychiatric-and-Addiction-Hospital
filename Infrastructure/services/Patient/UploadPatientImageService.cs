using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Patient
{
    public class UploadPatientImageService : IUploadPatientImage
    {
        private readonly AddIdentityDbContext _context;

        public UploadPatientImageService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PatientProfileResponse>> UploadImageAsync(string userId, string imageUrl, CancellationToken ct)
        {
            var profile = await _context.PatientProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId, ct);

            if (profile == null)
                return ResponseFactory.Fail<PatientProfileResponse>("Patient profile not found",
                    new List<string> { "No profile exists for the given userId." });

            profile.ImageUrl = imageUrl;
            await _context.SaveChangesAsync(ct);

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
            }, "Patient image uploaded successfully");
        }
    }
}
