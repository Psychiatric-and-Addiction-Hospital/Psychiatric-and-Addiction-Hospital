using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Attendance
{
    public class AttendanceLockService : IAttendanceLock
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public AttendanceLockService
            (AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        #region Lock

        public async Task<BaseResponse<string>> LockAsync(
            Guid attendanceId,
            CancellationToken ct)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(
                    x => x.Id == attendanceId, ct);

            if (attendance == null)
            {
                return ResponseFactory.Fail<string>(
                    "Attendance record not found.");
            }

            if (attendance.IsLocked)
            {
                return ResponseFactory.Fail<string>(
                    "Attendance is already locked.");
            }

            attendance.IsLocked = true;

            attendance.ModifiedAt = DateTime.UtcNow;

            attendance.ModifiedBy = _currentUser.UserId;

            attendance.ModificationReason =
                "Attendance locked by HR.";

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                "Attendance locked successfully.");
        }

        #endregion

        #region Unlock

        public async Task<BaseResponse<string>> UnlockAsync(
            Guid attendanceId,
            CancellationToken ct)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(
                    x => x.Id == attendanceId,
                    ct);

            if (attendance == null)
            {
                return ResponseFactory.Fail<string>(
                    "Attendance record not found.");
            }

            if (!attendance.IsLocked)
            {
                return ResponseFactory.Fail<string>(
                    "Attendance is already unlocked.");
            }

            attendance.IsLocked = false;

            attendance.ModifiedAt = DateTime.UtcNow;

            attendance.ModifiedBy = _currentUser.UserId;

            attendance.ModificationReason =
                "Attendance unlocked by HR.";

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                "Attendance unlocked successfully.");
        }

        #endregion
    }
}