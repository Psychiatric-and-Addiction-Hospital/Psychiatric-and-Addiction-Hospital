using Domain.Common;
using Domain.Enums.HR;
using System;

namespace Domain.Entites.HR.Leave
{
    public class LeaveRequest : BaseEntity
    {
        // Employee
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        // Leave Type
        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;
        // Leave Dates
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        // Request Reason
        public string Reason { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
        // Workflow
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        // Approval
        public Guid? ApprovedByEmployeeId { get; set; }
        public Employee? ApprovedByEmployee { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string? ManagerComment { get; set; }

    }
}
