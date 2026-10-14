using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IDeleteCandidate
    {
        Task<BaseResponse<CandidateResponse>> DeleteAsync(Guid id, CancellationToken ct);
    }
}
