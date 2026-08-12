using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.ApplicationOffer
{
    public class ApplicationOfferResponse
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid CandidateId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public Guid JobPostingId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string PositionName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public decimal OfferedSalary { get; set; }

        public DateTime OfferDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public OfferStatus Status { get; set; }

        public string? Notes { get; set; }

        public Guid? ApprovedByEmployeeId { get; set; }

        public string? ApprovedByEmployeeName { get; set; }

        public bool HasContract { get; set; }
    }
}
