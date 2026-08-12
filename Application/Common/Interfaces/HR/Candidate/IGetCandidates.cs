using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.Candidate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IGetCandidates
    {
        Task<BaseResponse<PagedResponse<CandidateResponse>>> GetAllAsync(GetCandidatesQuery request,CancellationToken ct);
    }
}
