using System;
using System.Collections.Generic;

namespace Application.DTOS.Responses
{
    public class SessionDetailResponse
    {
        public Guid Id { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public int DurationMinutes { get; set; }
        public string SessionType { get; set; }
        public string Status { get; set; }
        public string? CancellationReason { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public List<SessionNoteResponse> Notes { get; set; } = new();
    }
}
