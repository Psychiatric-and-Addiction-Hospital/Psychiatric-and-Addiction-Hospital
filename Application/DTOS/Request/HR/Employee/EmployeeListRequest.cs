using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Employee
{
    public class EmployeeListRequest
    {
        public string? Search { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }

        public Guid? ShiftId { get; set; }

        public string? Role { get; set; }

        public EmploymentStatus? EmploymentStatus { get; set; }

        public bool? IsActive { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
