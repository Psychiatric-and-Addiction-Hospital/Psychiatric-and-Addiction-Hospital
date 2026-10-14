using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using MediatR;

namespace Application.Queries.HR.Dashboard
{
    public record GetLeaveDashboardQuery() : IRequest<BaseResponse<LeaveDashboardResponse>>;
  
}
