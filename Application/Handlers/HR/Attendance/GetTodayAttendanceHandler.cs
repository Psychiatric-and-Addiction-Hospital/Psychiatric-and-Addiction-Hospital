using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using Application.Queries.HR.Attendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class GetTodayAttendanceHandler : IRequestHandler<GetTodayAttendanceQuery, BaseResponse<AttendanceResponse>>
    {
        private readonly IGetTodayAttendance _service;
        private readonly ICurrentUser _currentUser;

        public GetTodayAttendanceHandler(
            IGetTodayAttendance service,
            ICurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(GetTodayAttendanceQuery request, CancellationToken ct)
        {
            return await _service.GetTodayAttendanceAsync(_currentUser.UserId!, ct);
        }
    }
}
