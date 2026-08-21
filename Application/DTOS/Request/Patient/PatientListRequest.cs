using System.ComponentModel;

namespace Application.DTOS.Request.Patient
{
    public class PatientListRequest
    {
        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; } = false;

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
