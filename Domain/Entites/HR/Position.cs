using Domain.Common;
using Domain.Entites.HR.Recruitment;
using System;
using System.Collections.Generic;

namespace Domain.Entites.HR
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasicSalary { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
    }
}
