using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.Contract
{
    public class ContractResponse
    {
        public Guid Id { get; set; }

        public Guid OfferId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid CandidateId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public Guid JobPostingId { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string PositionName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal BaseSalary { get; set; }

        public DateTime? SignedDate { get; set; }

        public DateTime? ProbationEndDate { get; set; }

        public ContractType ContractType { get; set; }

        public ContractStatus Status { get; set; }

        public string? Terms { get; set; }
    }
}
