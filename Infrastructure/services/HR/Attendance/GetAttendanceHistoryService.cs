using Application.Common.Extensions;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Attendance;
using Application.DTOS.Responses.HR.Attendance;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Attendance
{
    public class GetAttendanceHistoryService : IGetAttendanceHistory
    {
        private readonly AddIdentityDbContext _context;

        public GetAttendanceHistoryService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<AttendanceHistoryResponse>> GetHistoryAsync
            (string appUserId, AttendanceHistoryRequest request, CancellationToken ct)
        {
            #region Query

            var query = _context.Attendances
                .AsNoTracking()
                .Include(a => a.Employee)
                .Where(a => a.Employee.AppUserId == appUserId)
                .AsQueryable();

            #endregion

            #region Filters

            if (request.FromDate.HasValue)
            {
                query = query.Where(a =>
                    a.AttendanceDate >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                query = query.Where(a =>
                    a.AttendanceDate <= request.ToDate.Value);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(a =>
                    a.AttendanceStatus == request.Status.Value);
            }

            #endregion

            #region Sorting

            query = request.Descending
                ? query.OrderByDescending(a => a.AttendanceDate)
                : query.OrderBy(a => a.AttendanceDate);

            #endregion

            #region Statistics

            var totalLateMinutes = await query.SumAsync(
                a => a.LateMinutes,
                ct);

            var totalWorkedTicks = await query.SumAsync(
                a => a.ActualWorkedTime.Ticks,
                ct);

            var totalOvertimeTicks = await query.SumAsync(
                a => a.Overtime.Ticks,
                ct);

            var totalPresentDays = await query.CountAsync(
                a => a.AttendanceStatus == AttendanceStatus.Present,
                ct);

            var totalLateDays = await query.CountAsync(
                a => a.AttendanceStatus == AttendanceStatus.Late,
                ct);

            var totalAbsentDays = await query.CountAsync(
                a => a.AttendanceStatus == AttendanceStatus.Absent,
                ct);

            #endregion

            #region Pagination
            var pagedAttendances = await query
                .Select(a => new AttendanceResponse
                {
                    Id = a.Id,

                    AttendanceDate = a.AttendanceDate,

                    CheckInTime = a.CheckInTime,

                    CheckOutTime = a.CheckOutTime,

                    AttendanceStatus = a.AttendanceStatus,

                    LateMinutes = a.LateMinutes,

                    EarlyLeaveMinutes = a.EarlyLeaveMinutes,

                    WorkedTime = a.ActualWorkedTime,

                    Overtime = a.Overtime,

                    Source = a.Source,

                    IsLocked = a.IsLocked,

                    Remarks = a.Remarks,
                })
                .ToPagedResponseAsync(
                    request.PageNumber,
                    request.PageSize,
                    ct);
            #endregion

            #region Response

            var response = new AttendanceHistoryResponse
            {
                Attendances = pagedAttendances,

                TotalLateMinutes = totalLateMinutes,

                TotalWorkedTime = TimeSpan.FromTicks(totalWorkedTicks),

                TotalOvertime = TimeSpan.FromTicks(totalOvertimeTicks),

                TotalPresentDays = totalPresentDays,

                TotalLateDays = totalLateDays,

                TotalAbsentDays = totalAbsentDays
            };

            return ResponseFactory.Success(response, "Attendance history retrieved successfully.");

            #endregion
        }
    }
}




