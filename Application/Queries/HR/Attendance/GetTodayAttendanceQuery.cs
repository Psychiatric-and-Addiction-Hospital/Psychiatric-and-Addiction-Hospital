using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using MediatR;

namespace Application.Queries.HR.Attendance
{
    public record GetTodayAttendanceQuery() : IRequest<BaseResponse<AttendanceResponse>>;

}
