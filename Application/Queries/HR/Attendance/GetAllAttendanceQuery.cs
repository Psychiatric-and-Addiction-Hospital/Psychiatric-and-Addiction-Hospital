using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.Attendance
{
    public record GetAllAttendanceQuery() : IRequest<BaseResponse<List<AttendanceResponse>>>;
}
