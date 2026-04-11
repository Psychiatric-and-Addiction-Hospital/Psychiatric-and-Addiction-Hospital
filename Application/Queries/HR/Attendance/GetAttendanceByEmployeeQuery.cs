using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Queries.HR.Attendance
{
    public record GetAttendanceByEmployeeQuery(Guid EmployeeId) : IRequest<BaseResponse<List<AttendanceResponse>>>;
}
