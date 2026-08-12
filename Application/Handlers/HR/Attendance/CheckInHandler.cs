using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    internal class CheckInHandler:IRequestHandler<CheckInCommand, BaseResponse<AttendanceResponse>>
    {
        private readonly ICheckInAttendance _service;

        private readonly ICurrentUser _currentUser;

        public CheckInHandler(
            ICheckInAttendance service,
            ICurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(
            CheckInCommand request,
            CancellationToken ct)
        {
            return await _service.CheckInAsync(_currentUser.UserId!,request.Request.Token,ct);
        }
    }
}
