using Domain.Enums;
using Domain.Enums.HR;
using System;

namespace Application.DTOS.Responses.HR.Employee
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string PositionName { get; set; } = string.Empty;

        public string ShiftName { get; set; } = string.Empty;

        public string EmployeeCode { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public EmploymentStatus EmploymentStatus { get; set; }

        public Guid? ManagerId { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public DateTime HireDate { get; set; }

        public string NationalId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhone { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }



    }
}
