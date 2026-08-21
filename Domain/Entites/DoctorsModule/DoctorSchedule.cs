using Domain.Common;
using System;

namespace Domain.Entites.DoctorsModule
{
    public class DoctorSchedule : BaseEntity
    {
        public Guid DoctorProfileId { get; set; }
        public DoctorProfile DoctorProfile { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsBooked { get; set; } 
    }
}