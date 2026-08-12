using System;

namespace Application.DTOS.Request.HR.Position
{
    public class UpdatePositionRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasicSalary { get; set; }
        public string EmployeeCodePrefix { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
