using Domain.Enums;
using System;

namespace Application.DTOS.Responses
{
    public class DoctorProfileResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }        
        public string ImagePath { get; set; }
        public Gender Gender { get; set; }
        public string DepartmentName { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
