using Domain.Enums.HR;
using System;

namespace Application.DTOS.Request.HR.Contract
{
    public class UpdateContractRequest
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal BaseSalary { get; set; }

        public DateTime? ProbationEndDate { get; set; }

        public ContractType ContractType { get; set; }

        public string? Terms { get; set; }
    }
}
