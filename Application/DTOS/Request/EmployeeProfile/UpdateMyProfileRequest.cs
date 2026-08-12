using System;

namespace Application.DTOS.Request.EmployeeProfile
{
    public class UpdateMyProfileRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhone { get; set; }

        public string? ImageUrl { get; set; }
    }
}
