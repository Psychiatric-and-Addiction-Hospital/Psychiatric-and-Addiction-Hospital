using Domain.Common;
using Domain.Entites.HR;
using System;
using System.Collections.Generic;


namespace Domain.Entites.DoctorsModule
{
    public class DoctorProfile: BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public string Specialization { get; set; } = string.Empty;

        public string LicenseNumber { get; set; } = string.Empty;

        public string Qualifications { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;

        public int YearsOfExperience { get; set; }

        public ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();

        public ICollection<PublicBooking> PublicBookings { get; set; } = new List<PublicBooking>();

    }
}
