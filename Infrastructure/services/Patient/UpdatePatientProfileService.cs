using Application.Commands.Patient;
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
    public class UpdatePatientProfileService : IUpdatePatientProfile
    {
        private readonly AddIdentityDbContext _context;

        public UpdatePatientProfileService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PatientProfileResponse>> UpdateAsync(UpdatePatientProfileCommand command, CancellationToken ct)
        {
            var profile = await _context.PatientProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == command.UserId, ct);

            if (profile == null)
                return ResponseFactory.Fail<PatientProfileResponse>("Patient profile not found",
                    new List<string> { "No profile exists for the given userId." });

            profile.FullName = command.FullName;
            profile.DateOfBirth = command.DateOfBirth;
            profile.Gender = command.Gender;
            profile.MaritalStatus = command.MaritalStatus;
            profile.Occupation = command.Occupation;
            profile.Address = command.Address;
            profile.PhoneNumber = command.PhoneNumber;

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
            }, "Patient profile updated successfully");
        }
    }
}
