using Application.Commands.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface ICreateJobPosting
    {
        Task<BaseResponse<JobPostingResponse>> CreateAsync(CreateJobPostingCommand request, CancellationToken ct);
    }
}
