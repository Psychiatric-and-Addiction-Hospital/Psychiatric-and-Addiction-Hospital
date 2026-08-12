using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Application.Queries.HR.JobPosting;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface IGetJobPostings
    {
        Task<BaseResponse<PagedResponse<JobPostingResponse>>> GetAllAsync(GetJobPostingsQuery request, CancellationToken ct);
    }
}
