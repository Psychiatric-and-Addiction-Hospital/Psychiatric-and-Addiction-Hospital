using Domain.Common;

namespace Domain.Entites
{
    public class Supporter : BaseEntity
    {
        public string FullName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PatientId { get; set; }
        public AppUser Patient { get; set; }
    }
}
