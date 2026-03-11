using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class PublicBookingResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid DoctorId { get; set; }
       public string DoctorName { get; set; }
        public DateTime PreferredDate { get; set; }
        public string PreferredTime { get; set; }

        public Status Status { get; set; }
    }
}
