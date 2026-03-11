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

        public DateTime Date { get; set; }

        public string Time { get; set; }
    }
}
