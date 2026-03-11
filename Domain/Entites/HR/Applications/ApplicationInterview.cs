using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR.Applications
{
   public  class ApplicationInterview:BaseEntity
    {//public string InterviewName { get; set; }   
        public DateTime ScheduledTime { get; set; }
        public string InterviewerName { get; set; }

        public string Feedback { get; set; }
    
        public int Score { get; set; } 



    }
}
