using Domain.Common;
using System;

namespace Domain.Entites.DoctorsModule
{
    public class DoctorSchedule : BaseEntity
    {
        public Guid DoctorProfileId { get; set; }
        public DoctorProfile DoctorProfile { get; set; }

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public bool IsBooked { get; set; } 
    }
}