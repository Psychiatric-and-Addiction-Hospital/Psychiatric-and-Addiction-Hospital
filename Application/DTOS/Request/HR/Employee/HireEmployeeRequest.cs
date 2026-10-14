using Application.Common.Enums;
using Application.DTOS.Request.Doctor;
using System;

namespace Application.DTOS.Request.HR.Employee
{
    public class HireEmployeeRequest
    {
        public Guid ContractId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid PositionId { get; set; }

        public Guid ShiftId { get; set; }

        public Guid? ManagerId { get; set; }

        public EmployeeRole Role { get; set; } 

        public DoctorProfileRequest? DoctorProfile { get; set; }
    }
}
