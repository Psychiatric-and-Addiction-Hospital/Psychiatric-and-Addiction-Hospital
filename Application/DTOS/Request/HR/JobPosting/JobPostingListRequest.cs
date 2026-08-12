using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.JobPosting
{
    public class JobPostingListRequest
    {
        public string? Search { get; set; }

        public Guid? DepartmentId { get; set; }

        public WorkMode? WorkMode { get; set; }

        public EmploymentType? EmploymentType { get; set; }

        public JobPostingStatus? Status { get; set; }

        public bool Descending { get; set; } = true;
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
