using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface ICreateCandidate
    {
        Task<BaseResponse<CandidateResponse>> CreateAsync
            (CreateCandidateRequest request, CancellationToken ct);
    }
}
