using Domain.Common;
using Domain.Enums.HR;
using System;

namespace Domain.Entites.HR.Recruitment
{
    public class ApplicationOffer : BaseEntity
    {
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; } = null!;

        public decimal OfferedSalary { get; set; }

        public DateTime OfferDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public OfferStatus Status { get; set; }
        public string? Notes { get; set; }
        public Contract? Contract { get; set; }
        public Guid? ApprovedByEmployeeId { get; set; }
        public Employee? ApprovedByEmployee { get; set; }
        public string? RejectionReason { get; set; }
        public string? OfferDocumentUrl { get; set; }
    }
}
