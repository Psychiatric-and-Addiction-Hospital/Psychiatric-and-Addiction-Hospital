using Application.Commands.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IManualAttendance
    {
        Task<BaseResponse<AttendanceResponse>> SaveAsync(ManualAttendanceCommand request, CancellationToken ct);
    }
}
