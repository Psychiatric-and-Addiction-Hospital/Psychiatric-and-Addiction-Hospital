using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface ICandidateInterview
    {
        Task<BaseResponse<List<CandidateInterviewResponse>>> GetUpcomingAsync(CancellationToken ct);
    }
}
