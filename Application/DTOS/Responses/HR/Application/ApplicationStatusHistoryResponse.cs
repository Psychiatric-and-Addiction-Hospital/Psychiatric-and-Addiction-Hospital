using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.Application
{
    public class ApplicationStatusHistoryResponse
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public ApplicationStatus Status { get; set; }

        public DateTime ChangedAt { get; set; }

        public string? Notes { get; set; }
    }
}
