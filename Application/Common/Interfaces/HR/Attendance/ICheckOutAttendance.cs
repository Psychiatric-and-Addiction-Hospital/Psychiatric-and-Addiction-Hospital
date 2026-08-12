using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface ICheckOutAttendance
    {
        Task<BaseResponse<AttendanceResponse>> CheckOutAsync
            (string appUserId, string qrToken, CancellationToken ct);
    }
}
