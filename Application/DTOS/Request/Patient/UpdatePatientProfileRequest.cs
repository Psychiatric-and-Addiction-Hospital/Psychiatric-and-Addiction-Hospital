using Domain.Enums;
using System;

namespace Application.DTOS.Request.Patient
{
    public class UpdatePatientProfileRequest
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
