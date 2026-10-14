using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Application.Queries.HR.Dashboard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Dashboard
{
    public class GetHRDashboardHandler : IRequestHandler<GetHRDashboardQuery, BaseResponse<HRDashboardResponse>>
    {
        private readonly IGetHRDashboard _service;
        public GetHRDashboardHandler(IGetHRDashboard service)
        {
            _service = service;
        }
        public async Task<BaseResponse<HRDashboardResponse>> Handle(GetHRDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
