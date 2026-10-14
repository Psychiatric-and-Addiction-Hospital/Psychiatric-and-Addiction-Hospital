using Domain.Common;
using System;

namespace Domain.Entites.HR.Performance
{
    public class PerformanceReviewItem:BaseEntity
    {
        public Guid PerformanceReviewId { get; set; }
        public PerformanceReview PerformanceReview { get; set; } = null!;
        public Guid PerformanceCriteriaId { get; set; }
        public PerformanceCriteria PerformanceCriteria { get; set; } = null!;
        public decimal Score { get; set; }
        public string? Comment { get; set; }
    }
}
