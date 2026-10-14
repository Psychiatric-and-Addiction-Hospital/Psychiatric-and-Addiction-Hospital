using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.DTOS.Responses.HR.JobPosting;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface ICreateJobPosting
    {
        Task<BaseResponse<JobPostingResponse>> CreateAsync(CreateJobPostingRequest request, CancellationToken ct);
    }
}
