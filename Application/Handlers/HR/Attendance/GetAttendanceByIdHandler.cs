using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Attendance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class GetAttendanceByIdHandler : IRequestHandler<GetAttendanceByIdQuery, BaseResponse<AttendanceResponse>>
    {
        private readonly IAttendanceService _attendanceService;

        public GetAttendanceByIdHandler(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(GetAttendanceByIdQuery request, CancellationToken ct)
        {
            return await _attendanceService.GetById(request.Id, ct);
        }
    }
}
