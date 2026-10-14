using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Dashboard
{
    public interface IGetHRDashboard
    {
        Task<BaseResponse<HRDashboardResponse>> GetAsync(CancellationToken ct);
    }
}
