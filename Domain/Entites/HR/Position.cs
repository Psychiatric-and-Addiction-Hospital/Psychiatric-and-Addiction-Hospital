using Domain.Common;
using Domain.Entites.HR.Recruitment;
using System;
using System.Collections.Generic;

namespace Domain.Entites.HR
{
    public class Position: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasicSalary { get; set; }
        public bool IsActive { get; set; } = true;
        public string EmployeeCodePrefix { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        // Employees currently occupying this position
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        // Job advertisements for this position
        public ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
    }
}
