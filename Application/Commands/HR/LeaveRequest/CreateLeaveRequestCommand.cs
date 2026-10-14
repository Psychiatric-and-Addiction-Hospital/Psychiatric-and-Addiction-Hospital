using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveRequest;
using Application.DTOS.Responses.HR.LeaveRequest;
using MediatR;

namespace Application.Commands.HR.LeaveRequest
{
    public record CreateLeaveRequestCommand(CreateLeaveRequest request) : IRequest<BaseResponse<LeaveRequestResponse>>;

}
