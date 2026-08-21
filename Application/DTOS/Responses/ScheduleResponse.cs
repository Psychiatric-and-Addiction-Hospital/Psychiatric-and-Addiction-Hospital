using System;

namespace Application.DTOS.Responses
{
    public class ScheduleResponse
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsBooked { get; set; }
    }
}
