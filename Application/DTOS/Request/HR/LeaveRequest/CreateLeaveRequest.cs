using System;

namespace Application.DTOS.Request.HR.LeaveRequest
{
    public class CreateLeaveRequest
    {
        public Guid LeaveTypeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
