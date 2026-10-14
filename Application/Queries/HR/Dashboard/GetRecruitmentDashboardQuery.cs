using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using MediatR;

namespace Application.Queries.HR.Dashboard
{
    public record GetRecruitmentDashboardQuery() : IRequest<BaseResponse<RecruitmentDashboardResponse>>;

}
