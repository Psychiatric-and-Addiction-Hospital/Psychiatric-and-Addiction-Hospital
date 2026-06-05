using Domain.Enums;
using System;

namespace Application.DTOS.Responses
{
    public class PatientProfileResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }
    }
}
