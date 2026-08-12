using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IGetCandidateById
    {
        Task<BaseResponse<CandidateResponse>> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
