using System;

namespace Application.DTOS.Responses
{
    public class SessionSummaryResponse
    {
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int DurationMinutes { get; set; }
        public string SessionType { get; set; }
        public string Status { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}
