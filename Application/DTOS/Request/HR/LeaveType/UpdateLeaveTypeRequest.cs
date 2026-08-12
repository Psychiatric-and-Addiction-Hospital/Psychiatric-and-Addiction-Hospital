using System;

namespace Application.DTOS.Request.HR.LeaveType
{
    public class UpdateLeaveTypeRequest
    {
        public Guid LeaveTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int MaxDaysPerYear { get; set; }

        public bool IsPaid { get; set; }

        public bool RequiresApproval { get; set; }

        public bool AllowHalfDay { get; set; }

        public bool IsActive { get; set; }
    }
}
