using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Shift
{
    public class ShiftListRequest
    {
        public string? Search { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsNightShift { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
