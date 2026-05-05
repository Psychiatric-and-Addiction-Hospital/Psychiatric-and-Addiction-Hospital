using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IGetAllCandidate
    {
        Task<BaseResponse<List<CandidateResponse>>> GetAllCandidatesAsync(CancellationToken ct);
    }
}
