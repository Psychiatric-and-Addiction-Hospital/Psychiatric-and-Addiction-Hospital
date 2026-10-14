using Application.Common.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.JobPosting
{
    public interface ICloseJobPosting
    {
        Task<BaseResponse<bool>> CloseAsync(Guid jobPostingId, CancellationToken ct);
    }
}
