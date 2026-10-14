using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.Application
{
    public class ApplicationResponse
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public Guid JobPostingId { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string PositionName { get; set; } = string.Empty;

        public DateTime AppliedDate { get; set; }

        public ApplicationStatus Status { get; set; }

        public string? Notes { get; set; }

        public string? CoverLetter { get; set; }

        public string ResumeSnapshotUrl { get; set; } = string.Empty;

        public int InterviewsCount { get; set; }

        public bool HasOffer { get; set; }

    }
}
