using System;

namespace Application.DTOS.Responses.HR
{
    public class CandidateResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ResumeUrl { get; set; }
    }
}
