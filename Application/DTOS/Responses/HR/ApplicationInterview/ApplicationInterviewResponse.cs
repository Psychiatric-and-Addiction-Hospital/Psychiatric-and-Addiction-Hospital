using Domain.Enums.HR;
using System;

namespace Application.DTOS.Responses.HR.ApplicationInterview
{
    public class ApplicationInterviewResponse
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid InterviewerId { get; set; }

        public string InterviewerName { get; set; } = string.Empty;

        public string CandidateName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public DateTime ScheduledAt { get; set; }

        public int DurationInMinutes { get; set; }

        public InterviewType InterviewType { get; set; }

        public InterviewStatus Status { get; set; }

        public InterviewResult? Result { get; set; }

        public int? Score { get; set; }

        public string? Location { get; set; }

        public string? MeetingLink { get; set; }

        public string? Feedback { get; set; }
    }
}
