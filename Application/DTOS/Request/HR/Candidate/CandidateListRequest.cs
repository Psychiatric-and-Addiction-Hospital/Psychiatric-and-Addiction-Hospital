using System.ComponentModel;

namespace Application.DTOS.Request.HR.Candidate
{
    public class CandidateListRequest
    {
        public string? Search { get; set; }

        public bool? IsActive { get; set; }

        public string? SortBy { get; set; }
        
        public bool Descending { get; set; }

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
