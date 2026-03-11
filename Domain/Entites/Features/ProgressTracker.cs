using Domain.Common;
using System;


namespace Domain.Entites.Features
{
    public class ProgressTracker:BaseEntity
    {
        
        public string Comment { get; set; }
        public DateTime SessionDate { get; set; } = DateTime.Now;
        public int Rating { get; set; }
        public Guid SessionId { get; set; }
        public Session Session { get; set; }
        public string PatientId { get; set; }
        public AppUser Patient { get; set; }
        
    }
}
