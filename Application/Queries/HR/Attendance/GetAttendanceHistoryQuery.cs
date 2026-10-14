using Application.Common.Responses;
using Application.DTOS.Request.HR.Attendance;
using Application.DTOS.Responses.HR.Attendance;
using MediatR;


namespace Application.Queries.HR.Attendance
{
    public record GetAttendanceHistoryQuery(AttendanceHistoryRequest Request) : IRequest<BaseResponse<AttendanceHistoryResponse>>;
}
