using Domain.Common;
using System;


namespace Domain.Entites
{
    public class Report: BaseEntity
    {
        public string DoctorId { get; set; }
     public AppUser Doctor { get; set; }

        public string PatientId { get; set; }
        public AppUser Patient { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; }

        public string Diagnosis { get; set; }          
        public string Notes { get; set; }             
        public string TreatmentPlan { get; set; }     
        public int ConditionRate { get; set; }         

        public string? AttachmentUrl { get; set; }     
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
