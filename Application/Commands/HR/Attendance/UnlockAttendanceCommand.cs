using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.Attendance
{
    public record UnlockAttendanceCommand(Guid AttendanceId) : IRequest<BaseResponse<string>>;
}
