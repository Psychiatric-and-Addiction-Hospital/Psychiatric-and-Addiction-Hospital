using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Application.Queries.HR.Dashboard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Dashboard
{
    public class GetAttendanceDashboardHandler : IRequestHandler<GetAttendanceDashboardQuery, BaseResponse<AttendanceDashboardResponse>>
    {
        private readonly IGetAttendanceDashboard _service;
        public GetAttendanceDashboardHandler(IGetAttendanceDashboard service)
        {
            _service = service;
        }
        public async Task<BaseResponse<AttendanceDashboardResponse>> Handle(GetAttendanceDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
