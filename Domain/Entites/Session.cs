using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Session:BaseEntity
    {
        public DateTime ScheduledDate { get; set; } 
        public int DurationMinutes { get; set; }
        public SessionType SessionType { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }
        public string? CancellationReason { get; set; }
        public string DoctorId { get; set; }
        public AppUser Doctor { get; set; }

        public string PatientId { get; set; }
        public AppUser Patient { get; set; }
        public ICollection<ProgressTracker> ProgressTrackers { get; set; } = new List<ProgressTracker>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
