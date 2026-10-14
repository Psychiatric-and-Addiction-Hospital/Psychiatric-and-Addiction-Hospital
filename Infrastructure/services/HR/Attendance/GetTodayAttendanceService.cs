using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Attendance
{
    public class GetTodayAttendanceService : IGetTodayAttendance
    {
        private readonly AddIdentityDbContext _context;

        public GetTodayAttendanceService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<AttendanceResponse>> GetTodayAttendanceAsync(
            string appUserId,
            CancellationToken ct)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var attendance = await _context.Attendances
                .AsNoTracking()
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(
                    a =>
                        a.Employee.AppUserId == appUserId &&
                        a.AttendanceDate == today,
                    ct);

            if (attendance == null)
            {
                return ResponseFactory.Fail<AttendanceResponse>(
                    "No attendance found for today.");
            }
            var response = new AttendanceResponse
            {
                Id = attendance.Id,
                AttendanceDate = attendance.AttendanceDate,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                AttendanceStatus = attendance.AttendanceStatus,
                LateMinutes = attendance.LateMinutes,
                WorkedTime = attendance.ActualWorkedTime,
                Overtime = attendance.Overtime,
            };

            return ResponseFactory.Success(response, "Today's attendance retrieved successfully.");

        }
    }
}