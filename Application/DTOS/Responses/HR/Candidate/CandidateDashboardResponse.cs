using Domain.Enums.HR;
using System;

namespace Application.DTOS.Responses.HR.Candidate
{
    public class CandidateDashboardResponse
    {
        public string CandidateName { get; set; } = string.Empty;

        public int ApplicationsCount { get; set; }

        public int ActiveApplicationsCount { get; set; }

        public int InterviewsCount { get; set; }

        public int UpcomingInterviewsCount { get; set; }

        public int PendingOffersCount { get; set; }

        public int AcceptedOffersCount { get; set; }

        public int RejectedOffersCount { get; set; }

        public ApplicationStatus? LatestApplicationStatus { get; set; }

        public DateTime? LatestApplicationStatusChangedAt { get; set; }
    }
}
