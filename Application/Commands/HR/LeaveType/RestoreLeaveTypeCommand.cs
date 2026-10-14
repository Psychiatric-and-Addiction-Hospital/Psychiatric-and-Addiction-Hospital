using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.LeaveType
{
    public record RestoreLeaveTypeCommand(Guid LeaveTypeId) : IRequest<BaseResponse<bool>>;
}
