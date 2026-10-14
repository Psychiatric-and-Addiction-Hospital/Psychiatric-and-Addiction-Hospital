using Application.Common.Responses;
using Application.DTOS.Request.HR.Attendance;
using Application.DTOS.Responses.HR.Attendance;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IGetAttendanceHistory
    {
        Task<BaseResponse<AttendanceHistoryResponse>> GetHistoryAsync(
           string appUserId, AttendanceHistoryRequest request, CancellationToken ct);
    }
}
