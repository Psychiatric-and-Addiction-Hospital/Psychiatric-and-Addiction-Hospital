using System;

namespace Application.DTOS.Responses.HR.Candidate
{
    public class CandidateResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }

        public int YearsOfExperience { get; set; }

        public string? CurrentCompany { get; set; }

        public string? CurrentPosition { get; set; }

        public decimal? ExpectedSalary { get; set; }

        public decimal? CurrentSalary { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? ResumeUrl { get; set; }

        public string? ImageUrl { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; }

        public string userId { get; set; }
    }
}
