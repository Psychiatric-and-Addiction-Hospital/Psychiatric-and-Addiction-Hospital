using Application.Common.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IAttendanceLock
    {
        Task<BaseResponse<string>> LockAsync(Guid attendanceId, CancellationToken ct);

        Task<BaseResponse<string>> UnlockAsync(Guid attendanceId, CancellationToken ct);
    }
}
