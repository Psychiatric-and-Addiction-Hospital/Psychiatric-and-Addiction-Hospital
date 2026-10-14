using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface IGetJobPostingById
    {
        Task<BaseResponse<JobPostingResponse>> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
