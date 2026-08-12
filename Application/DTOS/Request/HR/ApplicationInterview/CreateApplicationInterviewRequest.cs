using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.ApplicationInterview
{
    public class CreateApplicationInterviewRequest
    {
        public Guid ApplicationId { get; set; }

        public Guid InterviewerId { get; set; }

        public DateTime ScheduledAt { get; set; }

        public int DurationInMinutes { get; set; }

        public InterviewType InterviewType { get; set; }

        public string? Location { get; set; }

        public string? MeetingLink { get; set; }
    }
}
