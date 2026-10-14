
namespace Application.DTOS.Responses.HR.Dashboard
{
    public class DashboardSummaryResponse
    {        
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }

        public int TotalCandidates { get; set; }
        public int PublishedJobPostings { get; set; }
        public int closedJobPostings { get; set; }

        public int PresentToday { get; set; }
        public int LateToday { get; set; }
        public int AbsentToday { get; set; }

        public int PendingLeaveRequests { get; set; }
    }
}
