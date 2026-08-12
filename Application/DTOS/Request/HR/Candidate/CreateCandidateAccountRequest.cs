using System;

namespace Application.DTOS.Request.HR.Candidate
{
    public class CreateCandidateAccountRequest
    {
        public Guid CandidateId { get; set; }

        public string Token { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
