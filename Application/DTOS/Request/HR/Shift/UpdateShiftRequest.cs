using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Shift
{
    public class UpdateShiftRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public bool IsNightShift { get; set; }
        public int ToleranceMinutes { get; set; }
        public bool IsActive { get; set; }
    }
}
