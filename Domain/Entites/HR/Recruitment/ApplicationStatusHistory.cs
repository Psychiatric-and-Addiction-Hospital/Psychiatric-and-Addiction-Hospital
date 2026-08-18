using Domain.Common;
using Domain.Enums.HR;
using System;

namespace Domain.Entites.HR.Recruitment
{
    public class ApplicationStatusHistory : BaseEntity
    {
        public Guid ApplicationId { get; set; }

        public Application Application { get; set; } = null!;

        public ApplicationStatus Status { get; set; }

        public DateTime ChangedAt { get; set; }

        public string? Notes { get; set; }
    }

}
