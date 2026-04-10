using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR
{
    public  class Contract: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Terms { get; set; }// الشروط  
        public decimal BaseSalary { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }  

    }
}
