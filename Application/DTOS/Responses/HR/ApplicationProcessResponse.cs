using System;

namespace Application.DTOS.Responses.HR
{
    public class ApplicationProcessResponse
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid RecruitmentId { get; set; }

    }
}
