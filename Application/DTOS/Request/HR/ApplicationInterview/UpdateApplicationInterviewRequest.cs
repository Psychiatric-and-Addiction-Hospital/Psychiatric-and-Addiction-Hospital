using Domain.Enums.HR;
using System;

namespace Application.DTOS.Request.HR.ApplicationInterview
{
    public class UpdateApplicationInterviewRequest
    {
        public Guid Id { get; set; }

        public Guid InterviewerId { get; set; }

        public DateTime ScheduledAt { get; set; }

        public int DurationInMinutes { get; set; }

        public InterviewType InterviewType { get; set; }

        public string? Location { get; set; }

        public string? MeetingLink { get; set; }
    }
}
