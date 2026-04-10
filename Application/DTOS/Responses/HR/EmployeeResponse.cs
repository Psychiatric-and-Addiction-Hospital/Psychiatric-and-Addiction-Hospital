using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR
{
    public  class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid DepartmentId { get; set; }
        public bool IsActive { get; set; }

    }
}
