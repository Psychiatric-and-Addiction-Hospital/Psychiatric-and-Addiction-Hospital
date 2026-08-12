using System;

namespace Application.DTOS.Request.HR.Employee
{
    public class DeleteEmployeeRequest
    {
        public Guid EmployeeId { get; set; }

        public string? Reason { get; set; }
    }
}
