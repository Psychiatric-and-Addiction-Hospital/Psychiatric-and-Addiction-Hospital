using Domain.Common;
using Domain.Enums;
using System;


namespace Domain.Entites
{
    public class DoctorApplication: BaseDoctore
    { 
        
        public Status Status{ get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
    }
}
