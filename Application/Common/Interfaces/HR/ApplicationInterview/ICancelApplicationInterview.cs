using Application.Commands.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface ICancelApplicationInterview
    {
        Task<BaseResponse<ApplicationInterviewResponse>> CancelAsync(Guid InterviewId, CancellationToken ct);
    }
}
