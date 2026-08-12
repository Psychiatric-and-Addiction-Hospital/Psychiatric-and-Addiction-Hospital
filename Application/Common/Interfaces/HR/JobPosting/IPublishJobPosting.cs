using Application.Common.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface IPublishJobPosting
    {
        Task<BaseResponse<bool>> PublishAsync(Guid jobPostingId, CancellationToken ct);
    }
}
