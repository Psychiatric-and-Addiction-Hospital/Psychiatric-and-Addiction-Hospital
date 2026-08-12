using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class UnlockAttendanceHandler:IRequestHandler<UnlockAttendanceCommand, BaseResponse<string>>
    {
        private readonly IAttendanceLock _service;
        public UnlockAttendanceHandler(IAttendanceLock service)
        {
            _service = service;
        }

        public async Task<BaseResponse<string>> Handle(UnlockAttendanceCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnlockAsync(request.AttendanceId, cancellationToken);
        }
    }
}
