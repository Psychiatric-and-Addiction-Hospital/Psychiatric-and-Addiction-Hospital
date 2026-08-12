using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.JobPosting
{
    public class JobPostingResponse
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

        public JobPostingStatus Status { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public Guid PositionId { get; set; }

        public string PositionName { get; set; } = string.Empty;

        public Guid HiringManagerId { get; set; }

        public string HiringManagerName { get; set; } = string.Empty;
    }
}

