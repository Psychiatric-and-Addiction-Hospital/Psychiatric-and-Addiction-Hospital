using Domain.Common;
using Domain.Enums.HR;
using System;
using System.Collections.Generic;

namespace Domain.Entites.HR.Recruitment
{
    public class Application : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; } = null!;
        public Guid JobPostingId { get; set; }
        public JobPosting JobPosting { get; set; } = null!;
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
        public string? CoverLetter { get; set; }
        public string ResumeSnapshotUrl { get; set; } = string.Empty;
        // Interviews
        public ICollection<ApplicationInterview> Interviews { get; set; } = new List<ApplicationInterview>();
        // Final Offer
        public ApplicationOffer? Offer { get; set; }
    }
}
