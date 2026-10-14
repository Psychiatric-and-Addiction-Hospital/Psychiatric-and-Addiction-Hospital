using Domain.Common;
using Domain.Enums.HR;
using System;

namespace Domain.Entites.HR.Recruitment
{
    public class ApplicationInterview : BaseEntity
    {
        public Guid ApplicationId { get; set; }

        public Application Application { get; set; } = null!;

        public Guid InterviewerId { get; set; }

        public Employee Interviewer { get; set; } = null!;

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
