using Domain.Enums.HR;
using System;

namespace Application.DTOS.Request.HR.Application
{
    public class UpdateApplicationStatusRequest
    {
        public Guid Id { get; set; }

        public ApplicationStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}
