using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class DeleteAttendanceHandler : IRequestHandler<DeleteAttendanceCommand, BaseResponse<AttendanceResponse>>
    {
        private readonly IAttendanceService _attendanceService;

        public DeleteAttendanceHandler(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(DeleteAttendanceCommand request, CancellationToken ct)
        {
            return await _attendanceService.DeleteAttendance(request.Id, ct);
        }
    }
}
