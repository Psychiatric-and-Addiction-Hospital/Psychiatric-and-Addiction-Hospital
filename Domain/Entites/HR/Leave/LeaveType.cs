using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites.HR.Leave
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool AllowHalfDay { get; set; }
        // عدد الأيام الافتراضي
        public int MaxDaysPerYear { get; set; }
        // مدفوعة؟
        public bool IsPaid { get; set; }
        // هل تحتاج موافقة؟
        public bool RequiresApproval { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; } = new List<EmployeeLeaveBalance>();
    }
}
