using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.Candidate
{
    public class DeleteCandidateService : IDeleteCandidate
    {
        private readonly AddIdentityDbContext _Context;
        public DeleteCandidateService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<CandidateResponse>> DeleteCandidateAsync(Guid id, CancellationToken ct)
        {
            var Candidate = _Context.Candidates.FirstOrDefault(c => c.Id == id);
            if (Candidate is null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate not found");
            _Context.Candidates.Remove(Candidate);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success<CandidateResponse>(null, "Candidate deleted successfully.");
        }
    }
}
