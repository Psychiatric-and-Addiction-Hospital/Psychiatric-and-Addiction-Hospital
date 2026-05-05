using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entites.HR.Applications
{
    public class ApplicationInterview : BaseEntity
    {
        public Guid ApplicationProcessId { get; set; }
        public ApplicationProcess ApplicationProcess { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string InterviewerName { get; set; }
        public string Feedback { get; set; }
        public int Score { get; set; }
        public InterviewType interviewType { get; set; }
        public string Location { get; set; }
    }
}
