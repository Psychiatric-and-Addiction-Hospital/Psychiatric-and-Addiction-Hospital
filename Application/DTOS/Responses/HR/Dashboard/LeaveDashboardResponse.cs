
namespace Application.DTOS.Responses.HR.Dashboard
{
    public class LeaveDashboardResponse
    {
        public int PendingRequests { get; set; }

        public int ApprovedRequests { get; set; }

        public int RejectedRequests { get; set; }

        public int EmployeesOnLeaveToday { get; set; }

        public int LeaveRequestsThisMonth { get; set; }

        public double ApprovalRate { get; set; }
    }
}
