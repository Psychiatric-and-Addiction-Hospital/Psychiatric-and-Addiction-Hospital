using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Application.Queries.HR.ApplicationInterview;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationInterview
{
    public interface IGetApplicationInterviewById
    {
        Task<BaseResponse<ApplicationInterviewResponse>> GetByIdAsync(GetApplicationInterviewByIdQuery request, CancellationToken ct);
    }
}
