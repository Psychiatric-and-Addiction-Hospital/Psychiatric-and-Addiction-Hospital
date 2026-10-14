using Domain.Enums.HR;
using System;

namespace Application.DTOS.Responses.HR.Attendance
{
    public class AttendanceCalculationResultResponse
    {
        public int LateMinutes { get; set; }

        public int EarlyLeaveMinutes { get; set; }

        public TimeSpan WorkedTime { get; set; }

        public TimeSpan Overtime { get; set; }

        public AttendanceStatus Status { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

    }
}
