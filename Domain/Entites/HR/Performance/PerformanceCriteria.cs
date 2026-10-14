using Domain.Common;
using System.Collections.Generic;


namespace Domain.Entites.HR.Performance
{
    public class PerformanceCriteria:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // الدرجة القصوى لهذا المعيار
        public int MaxScore { get; set; } = 100;

        public bool IsActive { get; set; } = true;

        public ICollection<PerformanceReviewItem> ReviewItems { get; set; }= new List<PerformanceReviewItem>();
    }
}
