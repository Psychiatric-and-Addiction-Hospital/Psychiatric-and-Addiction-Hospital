using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.Queries.HR.Application;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Application
{
    public interface IGetApplications
    {
        Task<BaseResponse<PagedResponse<ApplicationResponse>>> GetAllAsync(GetApplicationsQuery request, CancellationToken ct);
    }
}
