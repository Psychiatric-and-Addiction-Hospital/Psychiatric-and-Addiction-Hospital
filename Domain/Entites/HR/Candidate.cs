using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR
{
    public  class Candidate: BaseEntity
    {
// frist_ last   public string FullName { get; set; }
        public string Email { get; set; }
        public string ResumeUrl { get; set; }


    }
}
