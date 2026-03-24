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
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public bool WasPresent => CheckIn != TimeSpan.Zero;


    }
}
