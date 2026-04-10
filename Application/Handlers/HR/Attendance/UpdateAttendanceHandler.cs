using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class UpdateAttendanceHandler : IRequestHandler<UpdateAttendanceCommand, BaseResponse<AttendanceResponse>>
    {
        private readonly IAttendanceService _attendanceService;

        public UpdateAttendanceHandler(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(UpdateAttendanceCommand request, CancellationToken ct)
        {
            return await _attendanceService.UpdateAttendance(request.Id, request.CheckOut, ct);
        }
    }
}
