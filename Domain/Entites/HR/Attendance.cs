using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR
{
    public  class Attendance:BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }
        public DateTime ? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
      

        private static int CalculateLateMinutes(DateTime checkIn)
        {
            var officialStartTime = checkIn.Date.AddHours(8);

            if (checkIn <= officialStartTime)
                return 0;
            return (int)(checkIn - officialStartTime).TotalMinutes;
        }

    }
}
