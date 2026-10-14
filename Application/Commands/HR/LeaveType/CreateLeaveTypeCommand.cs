using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.DTOS.Responses.HR.LeaveType;
using MediatR;

namespace Application.Commands.HR.LeaveType
{
    public record CreateLeaveTypeCommand(CreateLeaveTypeRequest LeaveType) : IRequest<BaseResponse<LeaveTypeResponse>>;
}
