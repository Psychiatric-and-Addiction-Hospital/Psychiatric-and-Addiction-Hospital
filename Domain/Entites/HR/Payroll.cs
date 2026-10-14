using Domain.Common;
using Domain.Enums.HR;
using System;

namespace Domain.Entites.HR
{
    public class Payroll:BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public PayrollType PayrollType { get; set; }

        public PayrollStatus Status { get; set; }

        public decimal Amount { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public string? Description { get; set; }

        public string? ReferenceNumber { get; set; }


    }
}
