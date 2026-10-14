using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class lockAttendanceHandler : IRequestHandler<LockAttendanceCommand, BaseResponse<string>>
    {
        private readonly IAttendanceLock _service;
        public async Task<BaseResponse<string>> Handle(LockAttendanceCommand request, CancellationToken ct)
        {
            return await _service.LockAsync(request.AttendanceId, ct);
        }
    }
}
