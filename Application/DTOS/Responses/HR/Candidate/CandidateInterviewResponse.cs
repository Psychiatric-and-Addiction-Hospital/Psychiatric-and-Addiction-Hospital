using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.Candidate
{
    public class CandidateInterviewResponse
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public string JobTitle { get; set; } = null!;

        public string PositionName { get; set; } = null!;

        public string DepartmentName { get; set; } = null!;

        public string InterviewerName { get; set; } = null!;

        public DateTime ScheduledAt { get; set; }

        public int DurationInMinutes { get; set; }

        public InterviewType InterviewType { get; set; }

        public InterviewStatus Status { get; set; }

        public string? Location { get; set; }

        public string? MeetingLink { get; set; }
    }
}
