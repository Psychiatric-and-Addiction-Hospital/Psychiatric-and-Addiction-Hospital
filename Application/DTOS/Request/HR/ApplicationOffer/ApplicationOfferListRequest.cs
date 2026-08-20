using Domain.Enums.HR;
using System;
using System.ComponentModel;

namespace Application.DTOS.Request.HR.ApplicationOffer
{
    public class ApplicationOfferListRequest
    {
        public string? Search { get; set; }

        public OfferStatus? Status { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }

        public Guid? ApprovedByEmployeeId { get; set; }

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public DateTime? FromOfferDate { get; set; }

        public DateTime? ToOfferDate { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; }

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
