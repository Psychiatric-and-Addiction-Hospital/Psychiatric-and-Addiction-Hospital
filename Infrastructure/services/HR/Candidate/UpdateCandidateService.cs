using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
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

namespace Infrastructure.services.HR.Candidate
{
    public class UpdateCandidateService : IUpdateCandidate
    {
        private readonly AddIdentityDbContext _context;
        private readonly IFileStorage _fileStorage;
        public UpdateCandidateService(AddIdentityDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }
        public async Task<BaseResponse<CandidateResponse>> UpdateAsync(UpdateCandidateRequest request, CancellationToken ct)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (candidate == null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate not found.");

            if (!candidate.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var exists = await _context.Candidates
                    .AnyAsync(x =>
                        x.Email == request.Email &&
                        x.Id != request.Id, ct);

                if (exists)
                    return ResponseFactory.Fail<CandidateResponse>("Email already exists.");
            }
            if (request.Image != null)
            {
                candidate.Image =
                    await _fileStorage.SaveFileAsync(
                        request.Image,
                        "candidate-images", ct);
            }

            if (request.Resume != null)
            {
                candidate.ResumeUrl =
                    await _fileStorage.SaveFileAsync(
                        request.Resume,
                        "candidate-resumes",
                        ct);
            }
            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Email = request.Email;
            candidate.PhoneNumber = request.PhoneNumber;
            candidate.DateOfBirth = request.DateOfBirth;
            candidate.YearsOfExperience = request.YearsOfExperience;
            candidate.CurrentCompany = request.CurrentCompany;
            candidate.CurrentPosition = request.CurrentPosition;
            candidate.CurrentSalary = request.CurrentSalary;
            candidate.ExpectedSalary = request.ExpectedSalary;
            candidate.LinkedInUrl = request.LinkedInUrl;
            candidate.Notes = request.Notes;
            candidate.IsActive = request.IsActive;

            await _context.SaveChangesAsync(ct);

            var response = new CandidateResponse
            {
                Id = candidate.Id,
                FullName = candidate.FullName,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                DateOfBirth = candidate.DateOfBirth,
                YearsOfExperience = candidate.YearsOfExperience,
                CurrentCompany = candidate.CurrentCompany,
                CurrentPosition = candidate.CurrentPosition,
                CurrentSalary = candidate.CurrentSalary,
                ExpectedSalary = candidate.ExpectedSalary,
                LinkedInUrl = candidate.LinkedInUrl,
                ResumeUrl = candidate.ResumeUrl,
                ImageUrl = candidate.Image,
                Notes = candidate.Notes,
                IsActive = candidate.IsActive
            };

            return ResponseFactory.Success(response, "Candidate updated successfully.");
        }
    }
}
