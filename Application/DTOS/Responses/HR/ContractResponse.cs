using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR
{
    public class ContractResponse
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Terms { get; set; }
        public decimal BaseSalary { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
