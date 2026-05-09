using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entites.HR
{
    public class Employee : BaseEntity
    {
        public AppUser user{ get; set; }
        public string UserId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Email { get; set; }
        public bool IsActive { get;   set; } 
        public List<Attendance> AttendanceLogs { get; set; }
        public List<Payroll> Payrolls { get; set; } = new();
        public Contract Contract { get; set; }

        public Guid? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public List<Recruitment> ManagedRecruitments { get; set; } = new();

    }
   
}

