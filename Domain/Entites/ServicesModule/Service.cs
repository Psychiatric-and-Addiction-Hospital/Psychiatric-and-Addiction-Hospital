using Domain.Common;
using Domain.Entites.HR;
using System;


namespace Domain.Entites.ServicesModule
{
    public class Service: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
