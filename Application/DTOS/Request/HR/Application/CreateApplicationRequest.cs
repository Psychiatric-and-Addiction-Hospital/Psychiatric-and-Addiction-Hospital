using System;

namespace Application.DTOS.Request.HR.Application
{
    public class CreateApplicationRequest
    {
        public Guid CandidateId { get; set; }

        public Guid JobPostingId { get; set; }

        public string? Notes { get; set; }

        public string? CoverLetter { get; set; }
    }
}
