using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Interfaces.UpLoad;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.CandidatePortal
{
    public class UpdateMyCandidateProfileService : IUpdateMyCandidateProfile
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorage _fileStorage;

        public UpdateMyCandidateProfileService(AddIdentityDbContext context, ICurrentUser currentUser, IFileStorage fileStorage)
        {
            _context = context;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
        }
        public async Task<BaseResponse<CandidateResponse>> UpdateAsync(UpdateCandidateProfileRequest request, CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<CandidateResponse>("User is not authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<CandidateResponse>("Current user was not found.");

            var candidate = await _context.Candidates
             .FirstOrDefaultAsync(
                 x => x.AppUserId == userId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate profile was not found.");

            candidate.FirstName = request.FirstName.Trim();

            candidate.LastName = request.LastName.Trim();

            candidate.PhoneNumber = request.PhoneNumber.Trim();

            candidate.Address = request.Address.Trim();

            candidate.DateOfBirth = request.DateOfBirth;

            candidate.YearsOfExperience = request.YearsOfExperience;

            candidate.CurrentCompany = request.CurrentCompany?.Trim();

            candidate.CurrentPosition = request.CurrentPosition?.Trim();

            candidate.CurrentSalary = request.CurrentSalary;

            candidate.ExpectedSalary = request.ExpectedSalary;

            candidate.LinkedInUrl = request.LinkedInUrl?.Trim();

            candidate.Notes = request.Notes?.Trim();

            if (request.Image != null)
            {
                var imageUrl = await _fileStorage.SaveFileAsync(
                    request.Image,
                    "candidate-images",
                    ct);

                candidate.Image = imageUrl;
            }

            if (request.Resume != null)
            {
                var resumeUrl = await _fileStorage.SaveFileAsync(
                    request.Resume,
                    "candidate-resumes",
                    ct);

                candidate.ResumeUrl = resumeUrl;
            }
            await _context.SaveChangesAsync(ct);
            var response = new CandidateResponse
            {
                Id = candidate.Id,

                FirstName = candidate.FirstName,

                LastName = candidate.LastName,

                FullName = candidate.FullName,

                Email = candidate.Email,

                PhoneNumber = candidate.PhoneNumber,

                DateOfBirth = candidate.DateOfBirth,

                YearsOfExperience =
                   candidate.YearsOfExperience,

                CurrentCompany =
                   candidate.CurrentCompany,

                CurrentPosition =
                   candidate.CurrentPosition,

                CurrentSalary =
                   candidate.CurrentSalary,

                ExpectedSalary =
                   candidate.ExpectedSalary,
                LinkedInUrl =
                    candidate.LinkedInUrl,

                ResumeUrl =
                    candidate.ResumeUrl,

                ImageUrl =
                    candidate.Image,

                Notes =
                    candidate.Notes,

                IsActive =
                    candidate.IsActive
            };

            return ResponseFactory.Success(response, "Candidate profile updated successfully.");
        }
    }

}
