using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
        public virtual Department Department { get; set; }

        public Guid HiringManagerId { get; set; }
      
        public virtual Employee HiringManager { get; set; }

    }
}
