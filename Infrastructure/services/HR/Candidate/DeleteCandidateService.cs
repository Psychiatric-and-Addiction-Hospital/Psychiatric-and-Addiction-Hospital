using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Candidate
{
    public class DeleteCandidateService : IDeleteCandidate
    {
        private readonly AddIdentityDbContext _context;
        public DeleteCandidateService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<CandidateResponse>> DeleteAsync(Guid id, CancellationToken ct)
        {
            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (candidate == null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate not found.");

            if (!candidate.IsActive)
                return ResponseFactory.Fail<CandidateResponse>("Candidate is already inactive.");

            candidate.IsActive = false;

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
                ExpectedSalary = candidate.ExpectedSalary,
                CurrentSalary = candidate.CurrentSalary,
                LinkedInUrl = candidate.LinkedInUrl,
                ResumeUrl = candidate.ResumeUrl,
                ImageUrl = candidate.Image,
                Notes = candidate.Notes,
                IsActive = candidate.IsActive
            };

            return ResponseFactory.Success(response, "Candidate deactivated successfully.");
        }
    }
}
