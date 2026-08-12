namespace Application.DTOS.Request.HR.Candidate
{
    public class CandidateListRequest
    {
        public string? Search { get; set; }

        public bool? IsActive { get; set; }

        public string? SortBy { get; set; }
        
        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
