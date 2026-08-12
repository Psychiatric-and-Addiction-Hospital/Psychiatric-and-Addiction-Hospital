using Domain.Enums.HR;
using System;

namespace Application.DTOS.Request.HR.Attendance
{
    public class ManualAttendanceRequest
    {
        public Guid EmployeeId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public AttendanceStatus? AttendanceStatus { get; set; }

        public string? Remarks { get; set; }

        public string? ModificationReason { get; set; }

        public bool LockAfterSave { get; set; }

    }
}
