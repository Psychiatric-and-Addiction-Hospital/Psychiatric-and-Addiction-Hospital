using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Dashboard
{
    public class GetDashboardSummaryService : IGetDashboardSummary
    {
        private readonly AddIdentityDbContext _context;

        public GetDashboardSummaryService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<DashboardSummaryResponse>> GetAsync(CancellationToken ct)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var response = new DashboardSummaryResponse
            {
                // Employees
                TotalEmployees = await _context.Employees.CountAsync(ct),

                ActiveEmployees = await _context.Employees
                    .CountAsync(x => x.IsActive, ct),

                InactiveEmployees = await _context.Employees
                    .CountAsync(x => !x.IsActive, ct),

                // Recruitment
                TotalCandidates = await _context.Candidates
                    .CountAsync(ct),

                PublishedJobPostings = await _context.JobPostings
                    .CountAsync(x => x.Status == JobPostingStatus.Published, ct),

                closedJobPostings = await _context.JobPostings
                    .CountAsync(x => x.Status == JobPostingStatus.Closed, ct),

                // Attendance
                PresentToday = await _context.Attendances
                    .CountAsync(x =>
                        x.AttendanceDate == today &&
                        x.CheckInTime != null, ct),

                LateToday = await _context.Attendances
                    .CountAsync(x =>
                        x.AttendanceDate == today &&
                        x.AttendanceStatus == AttendanceStatus.Late, ct),

                AbsentToday = await _context.Attendances
                    .CountAsync(x =>
                        x.AttendanceDate == today &&
                        x.AttendanceStatus == AttendanceStatus.Absent, ct),

                // Leave
                PendingLeaveRequests = await _context.LeaveRequests
                    .CountAsync(x =>
                        x.Status == LeaveStatus.Pending, ct)
            };

            return ResponseFactory.Success(response, "Dashboard summary retrieved successfully.");

        }
    }
}
