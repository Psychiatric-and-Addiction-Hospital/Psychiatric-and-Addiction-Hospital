using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.LeaveType
{
    public class LeaveTypeListRequest
    {
        public string? Search { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsPaid { get; set; }

        public bool? RequiresApproval { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

    }
}
