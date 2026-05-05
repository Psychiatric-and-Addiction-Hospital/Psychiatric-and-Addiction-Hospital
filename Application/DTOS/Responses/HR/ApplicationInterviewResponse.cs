using Domain.Entites.HR.Applications;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.HR
{
    public class ApplicationInterviewResponse
    {
        public Guid Id { get; set; }
        public Guid ApplicationProcessId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string InterviewerName { get; set; }
        public InterviewType InterviewType { get; set; }
        public string Location { get; set; }

    }
}
