using Domain.Enums.HR;
using System;

namespace Application.DTOS.Request.HR.ApplicationInterview
{
    public class CompleteInterviewRequest
    {
        public Guid Id { get; set; }

        public InterviewResult Result { get; set; }

        public int Score { get; set; }

        public string? Feedback { get; set; }
    }
}
