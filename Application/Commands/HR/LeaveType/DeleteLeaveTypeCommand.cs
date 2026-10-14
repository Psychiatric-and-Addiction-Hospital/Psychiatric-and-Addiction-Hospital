using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using MediatR;

namespace Application.Commands.HR.LeaveType
{
    public record DeleteLeaveTypeCommand(DeleteLeaveTypeRequest request) : IRequest<BaseResponse<bool>>;
}
