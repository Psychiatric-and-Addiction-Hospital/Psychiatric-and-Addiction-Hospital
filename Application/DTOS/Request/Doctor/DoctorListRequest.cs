using System;
using System.ComponentModel;

namespace Application.DTOS.Request.Doctor
{
    public class DoctorListRequest
    {
        public string? Search { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }

        public bool? IsActive { get; set; }

        public string? Specialization { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; } = false;

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
