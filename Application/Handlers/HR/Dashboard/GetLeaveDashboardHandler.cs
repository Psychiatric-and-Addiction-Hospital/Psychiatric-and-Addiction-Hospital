using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Application.Queries.HR.Dashboard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Dashboard
{
    public class GetLeaveDashboardHandler : IRequestHandler<GetLeaveDashboardQuery, BaseResponse<LeaveDashboardResponse>>
    {
        private readonly IGetLeaveDashboard _service;
        public GetLeaveDashboardHandler(IGetLeaveDashboard service)
        {
            _service = service;
        }
        public async Task<BaseResponse<LeaveDashboardResponse>> Handle(GetLeaveDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
