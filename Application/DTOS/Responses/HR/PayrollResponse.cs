using System;

namespace Application.DTOS.Responses.HR
{
    public class PayrollResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal OverSefit { get; set; }
        public decimal OvertimeSefite { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
    }
}
