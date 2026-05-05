using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IGetCandidateById
    {
        Task<BaseResponse<CandidateResponse>> GetCandidateByIdAsync(Guid id, CancellationToken ct);
    }
}
