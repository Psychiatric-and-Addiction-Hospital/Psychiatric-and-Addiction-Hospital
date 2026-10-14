using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.Attendance
{
    public record LockAttendanceCommand(Guid AttendanceId) : IRequest<BaseResponse<string>>;
}

