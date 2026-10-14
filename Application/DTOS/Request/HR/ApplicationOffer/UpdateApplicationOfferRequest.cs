using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.ApplicationOffer
{
    public class UpdateApplicationOfferRequest
    {
        public Guid Id { get; set; }

        public decimal OfferedSalary { get; set; }

        public DateTime OfferDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string? Notes { get; set; }

        public Guid? ApprovedByEmployeeId { get; set; }
    }
}
