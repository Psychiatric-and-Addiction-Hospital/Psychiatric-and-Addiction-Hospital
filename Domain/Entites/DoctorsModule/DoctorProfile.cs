using Domain.Common;

using Domain.Entites.HR;

using Domain.Entites.ServicesModule;

using System;
using System.Collections.Generic;


namespace Domain.Entites.DoctorsModule
{
    public class DoctorProfile: BaseDoctore
    {  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Guid EmployeeId { get; set; }    
        public Guid DepartmentId { get; set; } 

        public ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
        public ICollection<PublicBooking> PublicBookings { get; set; } = new List<PublicBooking>();

    }
}
