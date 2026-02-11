using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class DoctorApplication:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string CVFilePath { get; set; }
        public Status Status { get; set; }= Status.Pending;
        public DateTime AppliedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
    }
}
