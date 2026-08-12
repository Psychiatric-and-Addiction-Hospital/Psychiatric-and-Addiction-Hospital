using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;


namespace Domain.Entites.HR.Recruitment  
{
    public class Candidate : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int YearsOfExperience { get; set; }

        public string? CurrentCompany { get; set; }

        public string? CurrentPosition { get; set; }

        public decimal? ExpectedSalary { get; set; }

        public decimal? CurrentSalary { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Image { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? ResumeUrl { get; set; }

        public string? Notes { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
