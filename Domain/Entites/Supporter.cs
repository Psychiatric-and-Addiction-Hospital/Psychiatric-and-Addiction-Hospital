using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Supporter : BaseEntity
    {
        public string FullName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string patientId { get; set; }
        public AppUser patient { get; set; }
    }
}
