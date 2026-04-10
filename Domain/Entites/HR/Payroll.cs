using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR
{
    public class Payroll:BaseEntity
    {
        public Employee Employee { get; set; }  
        public Guid EmployeeId { get; set; }
     
        public DateTime PaymentDate { get; set; }
        public decimal OverSefit { get; set; } 
        public decimal OvertimeSefite { get; set; } 
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay =>( GrossPay + (OverSefit  * OvertimeSefite) ) - Deductions;
  
    }
}
