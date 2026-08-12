
namespace Application.DTOS.Request.HR.LeaveType
{
    public class CreateLeaveTypeRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int MaxDaysPerYear { get; set; }

        public bool IsPaid { get; set; }

        public bool RequiresApproval { get; set; }

        public bool AllowHalfDay { get; set; }
    }
}
