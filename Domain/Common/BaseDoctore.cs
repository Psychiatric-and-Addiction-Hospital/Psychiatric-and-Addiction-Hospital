using Domain.Enums;
using System.Text.Json.Serialization;


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
        public string Experience { get; set; }
        public string? ImagePath { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
