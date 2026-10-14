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
    public class GetLeaveDashboardService : IGetLeaveDashboard
    {
        private readonly AddIdentityDbContext _context;
        public GetLeaveDashboardService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<LeaveDashboardResponse>> GetAsync(CancellationToken ct)
        {
            var leaveSummary = await _context.LeaveRequests
                .AsNoTracking()
                .GroupBy(x => x.Status)

                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                }).ToListAsync(ct);

            var leaveRequests = leaveSummary.ToDictionary(
                x => x.Status,
                x => x.Count);

            var pendingRequests = leaveRequests.GetValueOrDefault(LeaveStatus.Pending);

            var approvedRequests = leaveRequests.GetValueOrDefault(LeaveStatus.Approved);

            var rejectedRequests = leaveRequests.GetValueOrDefault(LeaveStatus.Rejected);

            var now = DateTime.UtcNow;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var employeesOnLeaveToday =
                await _context.LeaveRequests
                    .AsNoTracking()
                    .CountAsync(x =>
                        x.Status == LeaveStatus.Approved &&
                        x.StartDate <= today &&
                        x.EndDate >= today, ct);

            

            var leaveRequestsThisMonth =
                await _context.LeaveRequests
                    .AsNoTracking()
                    .CountAsync(x =>
                        x.CreatedAt.Month == now.Month &&
                        x.CreatedAt.Year == now.Year, ct);

            var totalProcessed = approvedRequests + rejectedRequests;
            double approvalRate = totalProcessed == 0 ? 0 : (double)approvedRequests / totalProcessed * 100;

            var response = new LeaveDashboardResponse
            {
                PendingRequests = pendingRequests,

                ApprovedRequests = approvedRequests,

                RejectedRequests = rejectedRequests,

                EmployeesOnLeaveToday = employeesOnLeaveToday,

                LeaveRequestsThisMonth = leaveRequestsThisMonth,

                ApprovalRate = approvalRate
            };

            return ResponseFactory.Success(response, "Leave dashboard retrieved successfully.");
        }
    }
}
