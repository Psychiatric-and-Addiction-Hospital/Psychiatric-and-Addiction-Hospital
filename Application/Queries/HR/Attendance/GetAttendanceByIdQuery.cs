using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Queries.HR.Attendance
{
    public record GetAttendanceByIdQuery(Guid Id) : IRequest<BaseResponse<AttendanceResponse>>;
}
