using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
