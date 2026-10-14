using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class ManualAttendanceHandler : IRequestHandler<ManualAttendanceCommand, BaseResponse<AttendanceResponse>>
    {
        private readonly IManualAttendance _service;

        public ManualAttendanceHandler(IManualAttendance service)
        {
            _service = service;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(ManualAttendanceCommand request, CancellationToken ct)
        {
            return await _service.SaveAsync(request, ct);
        }
    }
}

