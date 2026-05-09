using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.booking
{
    public class RejectBookingResponse
    {
        public Guid Id { get; set; }
        public string? RejectionReason { get; set; }
        public Status Status { get; set; }
    }
}
