using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Contract
{
    public class ContractListRequest
    {
        public Guid? CandidateId { get; set; }

        public Guid? JobPostingId { get; set; }

        public ContractStatus? Status { get; set; }

        public ContractType? ContractType { get; set; }

        public DateTime? StartFrom { get; set; }

        public DateTime? StartTo { get; set; }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
