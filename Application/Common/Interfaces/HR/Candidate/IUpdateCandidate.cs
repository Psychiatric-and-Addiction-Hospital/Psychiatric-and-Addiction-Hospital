using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IUpdateCandidate
    {
        Task<BaseResponse<CandidateResponse>> UpdateAsync(UpdateCandidateRequest request, CancellationToken ct);
    }
}
