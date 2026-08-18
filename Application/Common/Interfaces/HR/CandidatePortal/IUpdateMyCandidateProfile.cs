using Application.Common.Responses;
using Application.DTOS.Request.HR.Candidate;
using Application.DTOS.Responses.HR.Candidate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface IUpdateMyCandidateProfile
    {
        Task<BaseResponse<CandidateResponse>> UpdateAsync(UpdateCandidateProfileRequest request, CancellationToken ct);
    }
}
