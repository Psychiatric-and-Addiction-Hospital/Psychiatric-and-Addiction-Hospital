
using Domain.Enums;

namespace Domain.Common
{
    public abstract class BaseDoctore : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string Qualifications { get; set; }
        public string ClinicAddress { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public Gender Gender { get; set; }
    }
}
