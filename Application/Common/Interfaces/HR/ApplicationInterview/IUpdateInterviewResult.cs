using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface IUpdateInterviewResult
    {
        Task<BaseResponse<ApplicationInterviewResponse>> UpdateInterviewResult(
            Guid interviewId, string feedback, int score,CancellationToken ct);
    }
}
