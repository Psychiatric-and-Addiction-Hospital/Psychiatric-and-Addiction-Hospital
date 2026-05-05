using Domain.Common;
using System;

namespace Domain.Entites.HR
{
    public  class Contract: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Terms { get; set; }
        public decimal BaseSalary { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }  

    }
}
