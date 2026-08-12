using Domain.Common;
using Domain.Enums.HR;
using System;

namespace Domain.Entites.HR
{
    public class Attendance : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public Guid ShiftId { get; set; }
        public Shift Shift { get; set; } = null!;

        public DateOnly AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public int LateMinutes { get; set; }

        public int EarlyLeaveMinutes { get; set; }

        public TimeSpan ActualWorkedTime { get; set; }

        public TimeSpan Overtime { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

        public AttendanceSource Source { get; set; }

        public bool IsLocked { get; set; }

        public string? Remarks { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string? ModificationReason { get; set; }
    }
}
