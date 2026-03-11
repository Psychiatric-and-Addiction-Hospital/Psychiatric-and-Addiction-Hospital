using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class PendingDoctorApplicationResponse
    {
        public Guid ApplicationId { get; set; }
        public string DoctorName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string specialization { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
