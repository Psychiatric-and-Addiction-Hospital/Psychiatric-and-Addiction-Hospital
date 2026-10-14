using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface ICheckInAttendance
    {
        Task<BaseResponse<AttendanceResponse>> CheckInAsync(
             string appUserId,
             string qrToken,
             CancellationToken ct);
    }
}
