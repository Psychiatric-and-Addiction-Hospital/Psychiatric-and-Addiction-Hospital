using Domain.Enums.HR;
using System;

namespace Application.DTOS.Request.HR.JobPosting
{
    public class UpdateJobPostingRequest
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public int Vacancies { get; set; }

        public WorkMode WorkMode { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid PositionId { get; set; }

        public Guid HiringManagerId { get; set; }
    }
}
