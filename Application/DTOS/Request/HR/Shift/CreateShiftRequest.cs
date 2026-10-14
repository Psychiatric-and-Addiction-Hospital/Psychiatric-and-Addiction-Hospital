using System;

namespace Application.DTOS.Request.HR.Shift
{
    public class CreateShiftRequest
    {
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public bool IsNightShift { get; set; }
        public int ToleranceMinutes { get; set; }
    }
}
