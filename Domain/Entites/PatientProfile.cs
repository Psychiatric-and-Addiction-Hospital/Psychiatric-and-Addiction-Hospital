using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entites
{
    public class PatientProfile : BaseEntity
    {
        public DateTime DateOfBirth { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
