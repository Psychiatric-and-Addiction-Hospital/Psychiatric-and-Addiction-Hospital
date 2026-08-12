using System;

namespace Application.DTOS.Request.HR.ApplicationOffer
{
    public class CreateApplicationOfferRequest
    {        
        public Guid ApplicationId { get; set; }
        public decimal OfferedSalary { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? Notes { get; set; }
        public Guid? ApprovedByEmployeeId { get; set; }
    }
}
