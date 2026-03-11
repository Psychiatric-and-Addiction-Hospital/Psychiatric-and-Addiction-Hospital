using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR.Applications
{
    public  class ApplicationOffer:BaseEntity
    {
        
        public decimal OfferedSalary { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsSigned { get; set; }


    }
}
