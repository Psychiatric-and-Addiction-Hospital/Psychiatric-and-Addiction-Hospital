using Domain.Enums.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR.LeaveRequest
{
    public class LeaveRequestResponse
    {
        public Guid Id { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string LeaveType { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int NumberOfDays { get; set; }

        public LeaveStatus Status { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string? ManagerComment { get; set; }

        public DateTime? DecisionDate { get; set; }

        public string? ApprovedBy { get; set; }
    }
}
