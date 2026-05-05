using Domain.Common;
using System;


namespace Domain.Entites.HR
{
    public  class Recruitment: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string ExperienceLevel { get; set; }
        public Guid DepartmentId { get; set; }
        public  Department Department { get; set; }
        public Guid HiringManagerId { get; set; }
        public  Employee HiringManager { get; set; }

    }
}
