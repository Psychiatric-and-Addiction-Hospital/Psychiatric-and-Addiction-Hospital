using System;

namespace Application.DTOS.Request.HR.manager
{
    public class ChangeDepartmentManagerRequest
    {
        public Guid DepartmentId { get; set; }

        public Guid NewManagerId { get; set; }
    }
}
