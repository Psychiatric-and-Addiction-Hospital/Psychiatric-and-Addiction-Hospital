using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Application.Queries.HR.Dashboard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Dashboard
{
    public class GetDashboardSummaryHandler : IRequestHandler<GetDashboardSummaryQuery, BaseResponse<DashboardSummaryResponse>>
    {
        private readonly IGetDashboardSummary _service;

        public GetDashboardSummaryHandler(IGetDashboardSummary service)
        {
            _service = service;
        }

        public async Task<BaseResponse<DashboardSummaryResponse>> Handle(GetDashboardSummaryQuery request, CancellationToken ct)
        {
            return await _service.GetAsync(ct);
        }
    }
}