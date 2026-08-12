using System;

namespace Application.DTOS.Responses.HR.Shift
{
    public class ShiftResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int BreakMinutes { get; set; }

        public bool IsNightShift { get; set; }

        public bool IsActive { get; set; }

        public int ToleranceMinutes { get; set; }
    }
}
