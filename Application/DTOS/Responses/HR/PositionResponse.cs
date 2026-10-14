using System;

namespace Application.DTOS.Responses.HR
{
    public class PositionResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal BasicSalary { get; set; }

        public bool IsActive { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
    }
}
