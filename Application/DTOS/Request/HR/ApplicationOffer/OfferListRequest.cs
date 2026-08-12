using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.ApplicationOffer
{
    public class OfferListRequest
    {
        public Guid? CandidateId { get; set; }

        public Guid? JobPostingId { get; set; }

        public OfferStatus? Status { get; set; }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        public int PageNamper { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
