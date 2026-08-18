using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface IGetMyCandidateProfile
    {
        Task<BaseResponse<CandidateResponse>> GetAsync(CancellationToken ct);
    }
}
