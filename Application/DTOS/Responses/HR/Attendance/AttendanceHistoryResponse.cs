using Application.Common.Responses;
using System;

namespace Application.DTOS.Responses.HR.Attendance
{
    public class AttendanceHistoryResponse
    {
        public PagedResponse<AttendanceResponse> Attendances { get; set; } = new();

        public int TotalLateMinutes { get; set; }

        public TimeSpan TotalWorkedTime { get; set; }

        public TimeSpan TotalOvertime { get; set; }

        public int TotalPresentDays { get; set; }

        public int TotalLateDays { get; set; }

        public int TotalAbsentDays { get; set; }
    }
}
