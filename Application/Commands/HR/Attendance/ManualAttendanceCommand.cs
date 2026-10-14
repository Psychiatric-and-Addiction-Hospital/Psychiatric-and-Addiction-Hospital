using Application.Common.Responses;
using Application.DTOS.Request.HR.Attendance;
using Application.DTOS.Responses.HR.Attendance;
using MediatR;

namespace Application.Commands.HR.Attendance
{
    public record ManualAttendanceCommand(ManualAttendanceRequest Request) : IRequest<BaseResponse<AttendanceResponse>>;


}
