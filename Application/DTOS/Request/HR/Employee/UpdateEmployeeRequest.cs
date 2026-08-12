using Application.DTOS.Request.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Employee
{
    public class UpdateEmployeeRequest
    {
        public Guid EmployeeId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid PositionId { get; set; }

        public Guid ShiftId { get; set; }

        public Guid? ManagerId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhone { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }

    }
}

