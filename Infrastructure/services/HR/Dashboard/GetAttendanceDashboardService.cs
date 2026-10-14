using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Dashboard
{
    public class GetAttendanceDashboardService : IGetAttendanceDashboard
    {
        private readonly AddIdentityDbContext _context;
        public GetAttendanceDashboardService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<AttendanceDashboardResponse>> GetAsync(CancellationToken ct)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var attendanceSummary = await _context.Attendances
                .AsNoTracking()
                .Where(x => x.AttendanceDate == today)
                .GroupBy(x => x.AttendanceStatus)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(ct);

            var attendance = attendanceSummary.ToDictionary(x => x.Status, x => x.Count);

            var present = attendance.GetValueOrDefault(AttendanceStatus.Present);
            var late = attendance.GetValueOrDefault(AttendanceStatus.Late);
            var absent = attendance.GetValueOrDefault(AttendanceStatus.Absent);
            var onLeave = attendance.GetValueOrDefault(AttendanceStatus.OnLeave);

            var totalEmployees = await _context.Employees.AsNoTracking()
                .CountAsync(x => x.IsActive
                && x.EmploymentStatus == EmploymentStatus.Active, ct);

            var attendanceRate = totalEmployees == 0 ? 0 : Math.Round(((double)(present + late) / totalEmployees) * 100, 2);

            var attendanceResponse = new AttendanceDashboardResponse
            {
                PresentToday = present,

                LateToday = late,

                AbsentToday = absent,

                OnLeaveToday = onLeave,

                AttendanceRate = attendanceRate
            };
            return ResponseFactory.Success(attendanceResponse, "Attendance Dashboard retrieved successfully.");


        }
    }
}
