using Domain.Common;
using System;


namespace Domain.Entites
{
    public class DoctorProfile: BaseDoctore
    {  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
    }
}
