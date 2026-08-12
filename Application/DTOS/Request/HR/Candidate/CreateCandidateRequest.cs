using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.DTOS.Request.HR.Candidate
{
    public class CreateCandidateRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public Gender Gender { get; set;  } 

        public DateOnly? DateOfBirth { get; set; }

        public int YearsOfExperience { get; set; }

        public string? CurrentCompany { get; set; }

        public string? CurrentPosition { get; set; }

        public decimal? ExpectedSalary { get; set; }

        public decimal? CurrentSalary { get; set; }

        public string? LinkedInUrl { get; set; }

        public IFormFile? Resume { get; set; }

        public string? Notes { get; set; }

        public IFormFile? Image { get; set; }
    }
}
