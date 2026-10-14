using System;

namespace Application.DTOS.Request.Patient
{
    public class CreatePublicBookingRequest
    {
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public Guid doctorId { get; set; }
        public Guid ScheduleId { get; set; }
        public string notes { get; set; }
    }
}
