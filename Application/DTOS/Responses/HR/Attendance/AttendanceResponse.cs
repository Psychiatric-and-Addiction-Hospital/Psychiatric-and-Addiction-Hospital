using Domain.Enums.HR;
using System;

namespace Application.DTOS.Responses.HR.Attendance
{
    public class AttendanceResponse
    {
        public Guid Id { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

        public int LateMinutes { get; set; }

        public int EarlyLeaveMinutes { get; set; }

        public TimeSpan WorkedTime { get; set; }

        public TimeSpan Overtime { get; set; }

        public AttendanceSource Source { get; set; }

        public bool IsLocked { get; set; }

        public string? Remarks { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}

