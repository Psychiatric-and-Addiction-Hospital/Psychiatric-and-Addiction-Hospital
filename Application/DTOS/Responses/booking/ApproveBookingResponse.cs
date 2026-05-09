using Domain.Enums;
using System;

namespace Application.DTOS.Responses.booking
{
    public class ApproveBookingResponse
    {
        public Guid BookingId { get; set; }
        public Status BookingStatus { get; set; }

        public Guid SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public SessionStatus SessionStatus { get; set; }

        public string DoctorName { get; set; }
        public string PatientName { get; set; }

        public string Message { get; set; }
    }
}
