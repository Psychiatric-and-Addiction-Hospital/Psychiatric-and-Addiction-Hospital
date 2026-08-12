using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using jobPosting = Domain.Entites.HR.Recruitment.JobPosting;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface IJobPostingValidation
    {
        Task<BaseResponse<bool>> ValidateCreateAsync(CreateJobPostingRequest request, CancellationToken ct);

        Task<BaseResponse<jobPosting>> ValidateUpdateAsync(UpdateJobPostingRequest Request, CancellationToken ct);

        Task<BaseResponse<jobPosting>> ValidateStatusChangeAsync(Guid jobPostingId, CancellationToken ct);


    }
}
