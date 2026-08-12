namespace Application.DTOS.Responses.HR.Dashboard
{
    public class RecruitmentDashboardResponse
    {
        public int PublishedJobPostings { get; set; }

        public int ClosedJobPostings { get; set; }

        public int TotalCandidates { get; set; }

        public int ApplicationsReceived { get; set; }

        public int InterviewsScheduled { get; set; }

        public int OffersSent { get; set; }

        public int HiredCandidates { get; set; }
    }
}
