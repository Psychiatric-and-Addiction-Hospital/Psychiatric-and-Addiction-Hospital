using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Attendance;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class GetAttendanceByEmployeeHandler : IRequestHandler<GetAttendanceByEmployeeQuery, BaseResponse<List<AttendanceResponse>>>
    {
        private readonly IAttendanceService _attendanceService;

        public GetAttendanceByEmployeeHandler(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public async Task<BaseResponse<List<AttendanceResponse>>> Handle(GetAttendanceByEmployeeQuery request, CancellationToken ct)
        {
            return await _attendanceService.GetByEmployee(request.EmployeeId, ct);
        }
    }
}
