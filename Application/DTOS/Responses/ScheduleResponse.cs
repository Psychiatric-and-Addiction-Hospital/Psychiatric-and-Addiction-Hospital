using System;

namespace Application.DTOS.Responses
{
    public class ScheduleResponse
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public bool IsBooked { get; set; }
    }
}
