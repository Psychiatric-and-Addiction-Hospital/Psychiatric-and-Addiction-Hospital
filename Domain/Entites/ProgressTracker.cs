using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ProgressTracker:BaseEntity
    {
        
        public string Comment { get; set; }
        public DateTime SessionDate { get; set; } = DateTime.Now;
        public int Rating { get; set; }//1:5
        public Guid SessionId { get; set; }
        public Session Session { get; set; }
        public string PatientId { get; set; }
        public AppUser Patient { get; set; }
        
    }
}
