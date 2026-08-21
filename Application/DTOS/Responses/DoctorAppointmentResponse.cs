using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class DoctorAppointmentResponse
    {
        public Guid AppointmentId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
    }
}
