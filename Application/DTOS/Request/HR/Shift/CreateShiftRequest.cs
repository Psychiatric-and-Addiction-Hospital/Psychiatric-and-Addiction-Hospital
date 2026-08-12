using System;

namespace Application.DTOS.Request.HR.Shift
{
    public class CreateShiftRequest
    {
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public bool IsNightShift { get; set; }
        public int ToleranceMinutes { get; set; }
    }
}
