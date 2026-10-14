using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.ApplicationInterview
{
    public class ApplicationInterviewListRequest
    {
        public string? Search { get; set; }

        public Guid? InterviewerId { get; set; }

        public Guid? ApplicationId { get; set; }

        public InterviewType? InterviewType { get; set; }

        public InterviewStatus? Status { get; set; }

        public InterviewResult? Result { get; set; }

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
