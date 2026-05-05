using Domain.Enums;
using System;

namespace Application.DTOS.Responses.HR
{
    public class ApplicationOfferResponse
    {
        public Guid Id { get; set; }
        public Guid ApplicationProcessId { get; set; }
        public decimal OfferedSalary { get; set; }
        public DateTime ExpiryDate { get; set; }
        public OfferStatues Statues {  get; set; }
    }
}
