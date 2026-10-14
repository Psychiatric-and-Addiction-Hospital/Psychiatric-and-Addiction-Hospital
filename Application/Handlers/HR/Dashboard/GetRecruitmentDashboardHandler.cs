using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Application.Queries.HR.Dashboard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Dashboard
{
    public class GetRecruitmentDashboardHandler : IRequestHandler<GetRecruitmentDashboardQuery, BaseResponse<RecruitmentDashboardResponse>>
    {
        private readonly IGetRecruitmentDashboard _service;
        public GetRecruitmentDashboardHandler(IGetRecruitmentDashboard service)
        {
            _service = service;
        }
        public async Task<BaseResponse<RecruitmentDashboardResponse>> Handle(GetRecruitmentDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(cancellationToken);
        }
    }
}
