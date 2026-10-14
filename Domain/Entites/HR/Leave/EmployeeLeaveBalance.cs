using Domain.Common;
using System;

namespace Domain.Entites.HR.Leave
{
    public class EmployeeLeaveBalance:BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public Guid LeaveTypeId { get; set; }

        public LeaveType LeaveType { get; set; } = null!;

        public int TotalDays { get; set; }

        public int UsedDays { get; set; }

        public int RemainingDays { get; set; }
    }
}
