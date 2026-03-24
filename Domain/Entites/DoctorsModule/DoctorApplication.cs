using Domain.Common;
using Domain.Enums;
using System;
using System.Text.Json.Serialization;


namespace Domain.Entites.DoctorsModule
{
    public class DoctorApplication: BaseDoctore
    {


        public Status Status{ get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
       
        
        
    }
}
