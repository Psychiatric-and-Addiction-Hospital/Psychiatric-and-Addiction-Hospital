using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Candidate
{
    public class GetCandidateByIdService : IGetCandidateById
    {
        private readonly AddIdentityDbContext _context;
        public GetCandidateByIdService(
            AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<CandidateResponse>> GetByIdAsync(
            Guid id,
            CancellationToken ct)
        {
            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (candidate == null)
            {
                return ResponseFactory.Fail<CandidateResponse>(
                    "Candidate not found.");
            }

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

            return ResponseFactory.Success(response, "Candidate retrieved successfully.");
        }
    }
}

