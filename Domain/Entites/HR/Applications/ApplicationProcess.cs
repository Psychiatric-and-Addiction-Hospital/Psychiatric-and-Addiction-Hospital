using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.HR.Applications
{
    public  class ApplicationProcess: BaseEntity
    {
        public Guid CandidateId { get; set; }
        public Guid RecruitmentId { get; set; }
        public ApplicationStatus States { get; set; }
        public List<ApplicationInterview> Interviews { get; set; } = new();
        public ApplicationOffer Offer { get; set; }


    }
}
