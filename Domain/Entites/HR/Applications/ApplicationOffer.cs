using Domain.Common;
using Domain.Enums;
using System;


namespace Domain.Entites.HR.Applications
{
    public class ApplicationOffer:BaseEntity
    {

        public Guid ApplicationProcessId { get; set; }    
        public ApplicationProcess ApplicationProcess { get; set; }
        public bool IsAccepted { get; set; }
        public decimal OfferedSalary { get; set; }
        public DateTime ExpiryDate { get; set; }
   
        public OfferStatues statues { get; set; }
    }
}
