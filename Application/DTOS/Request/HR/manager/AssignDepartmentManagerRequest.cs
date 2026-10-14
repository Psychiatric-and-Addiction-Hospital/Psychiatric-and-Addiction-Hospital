using System;

namespace Application.DTOS.Request.HR.manager
{
    public class AssignDepartmentManagerRequest
    {

        public Guid DepartmentId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
