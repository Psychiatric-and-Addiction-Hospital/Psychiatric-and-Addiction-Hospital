using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Attendance
{
    public record CreateAttendanceCommand(Guid EmployeeId) : IRequest<BaseResponse<object>>;
}
