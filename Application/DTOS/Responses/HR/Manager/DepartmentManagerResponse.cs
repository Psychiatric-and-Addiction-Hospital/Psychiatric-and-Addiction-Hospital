using System;

namespace Application.DTOS.Responses.HR.Manager
{
    public class DepartmentManagerResponse
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public Guid? ManagerId { get; set; }

        public string? ManagerName { get; set; }
    }
}
