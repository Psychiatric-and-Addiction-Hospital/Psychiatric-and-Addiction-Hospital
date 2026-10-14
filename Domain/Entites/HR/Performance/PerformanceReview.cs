using Domain.Common;
using Domain.Enums.HR;
using System;
using System.Collections.Generic;

namespace Domain.Entites.HR.Performance
{
    public class PerformanceReview:BaseEntity
    {
        // الموظف الذى يتم تقييمه
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        // المقيم
        public Guid ReviewerId { get; set; }
        public Employee Reviewer { get; set; } = null!;
        public DateOnly ReviewDate { get; set; }
        public PerformanceReviewStatus Status { get; set; }
        public string? GeneralComment { get; set; }
        public decimal OverallScore { get; set; }
        public ICollection<PerformanceReviewItem> Items { get; set; }= new List<PerformanceReviewItem>();
    }
}
