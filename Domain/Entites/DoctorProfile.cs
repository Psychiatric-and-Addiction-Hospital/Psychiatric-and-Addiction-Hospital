using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class DoctorProfile:BaseEntity
    {
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string LicenseNumber { get; set; }
        public string NationalId { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        

    }
}
