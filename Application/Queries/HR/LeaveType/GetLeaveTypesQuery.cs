using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.DTOS.Responses.HR.LeaveType;
using MediatR;

namespace Application.Queries.HR.LeaveType
{
    public record GetLeaveTypesQuery(LeaveTypeListRequest request) : IRequest<BaseResponse<PagedResponse<LeaveTypeResponse>>>;
}
