using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Application
{
    public class ApplicationListRequest
    {
        public string? Search { get; set; }

        public ApplicationStatus? Status { get; set; }

        public Guid? JobPostingId { get; set; }

        public Guid? CandidateId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
