using System;

namespace Application.DTOS.Request.Doctor
{
    public class CreateDoctorRequest
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
