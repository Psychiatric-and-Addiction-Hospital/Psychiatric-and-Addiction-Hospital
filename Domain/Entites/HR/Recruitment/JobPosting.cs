using Domain.Common;
using Domain.Enums.HR;
using System;
using System.Collections.Generic;


namespace Domain.Entites.HR.Recruitment
{
    public class JobPosting : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

        public int Vacancies { get; set; } = 1;
        public WorkMode WorkMode { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public DateTime PublishedDate { get; set; }
        public DateTime ClosingDate { get; set; }

        public JobPostingStatus Status { get; set; } = JobPostingStatus.Draft;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public Guid PositionId { get; set; }
        public Position Position { get; set; } = null!;

        public Guid? HiringManagerId { get; set; }
        public Employee? HiringManager { get; set; } = null!;

        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
