using System;

namespace Application.DTOS.Responses.HR.Candidate
{
    public class CandidateAccountResponse
    {
        public Guid CandidateId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool EmailConfirmed { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
