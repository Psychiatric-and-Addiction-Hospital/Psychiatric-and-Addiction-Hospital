using Domain.Common;
using Domain.Entites.DoctorsModule;
using Domain.Entites.HR.Recruitment;
using Domain.Entites.ServicesModule;
using System;
using System.Collections.Generic;
namespace Domain.Entites.HR
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        // Department Manager (Optional)
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        // Navigation Properties
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Position> Positions { get; set; } = new List<Position>();
        public ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
        public List<Service> Services { get; set; } = new List<Service>();

    }
}
 