using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using System;
using System.Threading;
using System.Threading.Tasks;
using Interview = Domain.Entites.HR.Recruitment.ApplicationInterview;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface IApplicationInterviewValidation
    {
        Task<BaseResponse<bool>> ValidateCreateAsync(CreateApplicationInterviewRequest request, CancellationToken ct);

        Task<BaseResponse<Interview>> ValidateUpdateAsync(UpdateApplicationInterviewRequest request, CancellationToken ct);

        Task<BaseResponse<Interview>> ValidateCompleteAsync(CompleteInterviewRequest request, CancellationToken ct);

        Task<BaseResponse<Interview>> ValidateCancelAsync(Guid interviewId, CancellationToken ct);
    }
}
