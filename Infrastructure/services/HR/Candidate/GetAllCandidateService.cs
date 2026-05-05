using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Candidate
{
    public class GetAllCandidateService : IGetAllCandidate
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllCandidateService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<CandidateResponse>>> GetAllCandidatesAsync(CancellationToken ct)
        {
            var Candidate = await _Context.Candidates.Select(c => new CandidateResponse
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                ResumeUrl = c.ResumeUrl
            }).ToListAsync(ct);
            if (Candidate is null || Candidate.Count == 0)
                return ResponseFactory.Fail<List<CandidateResponse>>("Candidate Not found.");


            return ResponseFactory.Success(Candidate, "All Candidate have been fetched successfully.");
        }
    }
}
