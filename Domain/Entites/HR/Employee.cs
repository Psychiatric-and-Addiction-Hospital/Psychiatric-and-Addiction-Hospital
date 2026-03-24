using Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR
{
    public class Employee : BaseEntity
    {
        public string  userId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid? CandidateId { get; set; }
   
        public string Email { get; set; }
        public bool IsActive { get;   set; } 
        public List<Attendance> AttendanceLogs { get; set; }

    
    }
    }

