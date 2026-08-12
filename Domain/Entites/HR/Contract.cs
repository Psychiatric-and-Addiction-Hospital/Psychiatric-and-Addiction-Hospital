using Domain.Common;
using Domain.Entites.HR.Recruitment;
using Domain.Enums.HR;
using System;


namespace Domain.Entites.HR
{
    public class Contract : BaseEntity
    {
        public Guid OfferId { get; set; }
        public ApplicationOffer Offer { get; set; } = null!;
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
