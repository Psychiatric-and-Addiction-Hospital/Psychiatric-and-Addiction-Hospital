using Application.Commands.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface IUpdateJobPosting
    {
        Task<BaseResponse<JobPostingResponse>> UpdateAsync(UpdateJobPostingCommand request, CancellationToken ct);
    }
}
