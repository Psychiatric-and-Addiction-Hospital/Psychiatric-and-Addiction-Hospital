using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Request.HR.Position
{
    public class CreatePositionRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasicSalary { get; set; }
        public string EmployeeCodePrefix { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
