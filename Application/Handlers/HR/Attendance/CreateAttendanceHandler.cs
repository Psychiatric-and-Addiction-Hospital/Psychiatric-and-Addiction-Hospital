using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class CreateAttendanceHandler : IRequestHandler<CreateAttendanceCommand, BaseResponse<object>>
    {
        private readonly IAttendanceService _attendanceService;

        public CreateAttendanceHandler(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public async Task<BaseResponse<object>> Handle(CreateAttendanceCommand request, CancellationToken ct)
        {
            return await _attendanceService.CreateAttendance(request.EmployeeId, ct);
        }
    }
}
