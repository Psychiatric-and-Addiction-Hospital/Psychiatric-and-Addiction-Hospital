using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.Dashboard
{
    public class AttendanceDashboardResponse
    {
        public int PresentToday { get; set; }

        public int LateToday { get; set; }

        public int AbsentToday { get; set; }

        public int OnLeaveToday { get; set; }

        public double AttendanceRate { get; set; }
    }
}
