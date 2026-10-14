using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface IGetApplicationInterviews
    {
        Task<BaseResponse<PagedResponse<ApplicationInterviewResponse>>> GetAllAsync(ApplicationInterviewListRequest request, CancellationToken ct);
    }
}
