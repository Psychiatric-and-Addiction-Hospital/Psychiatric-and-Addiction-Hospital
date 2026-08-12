using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entites.HR
{
    public class Shift:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int BreakMinutes { get; set; }=60;

        public bool IsNightShift { get; set; }

        public bool IsActive { get; set; } = true;
        // Minutes allowed before employee is considered late
        public int ToleranceMinutes { get; set; } = 15;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
