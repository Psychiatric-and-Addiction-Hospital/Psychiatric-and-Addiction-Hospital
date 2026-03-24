using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
  public   enum ApplicationStatus
    { 
        Applied = 1,

       
        PhoneScreen = 2,
        Interviewing = 3,
        TechnicalAssessment = 4,

        Offered = 5,
        Accepted = 6,

       
        Rejected = 7,
        Withdrawn = 8, 
        Blacklisted = 9 
    }
}
