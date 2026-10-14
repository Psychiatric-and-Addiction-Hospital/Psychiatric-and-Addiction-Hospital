using System;

namespace Application.DTOS.Responses.HR.LeaveType
{
    public class LeaveTypeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int MaxDaysPerYear { get; set; }

        public bool IsPaid { get; set; }

        public bool RequiresApproval { get; set; }

        public bool AllowHalfDay { get; set; }

        public bool IsActive { get; set; }
    }
}
