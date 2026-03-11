using Domain.Common;
using Domain.Entites.DoctorsModule;
using Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace Domain.Entites
{
    public class PublicBooking:BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public DateTime PreferredDate { get; set; }
        public string PreferredTime { get; set; } 

        public string Notes { get; set; }

        public Guid DoctorId { get; set; }
        public DoctorProfile Doctor { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } = Status.Pending;
    }
}
