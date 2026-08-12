using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IGetTodayAttendance
    {
        Task<BaseResponse<AttendanceResponse>> GetTodayAttendanceAsync(string appUserId, CancellationToken ct);
    }
}
