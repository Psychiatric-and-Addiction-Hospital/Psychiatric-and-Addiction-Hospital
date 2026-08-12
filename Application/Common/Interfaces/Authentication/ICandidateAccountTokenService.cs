using System;


namespace Application.Common.Interfaces.Authentication
{
    public interface ICandidateAccountTokenService
    {
        string GenerateToken(Guid candidateId, string email);

        bool ValidateToken(string token, Guid candidateId, string email);
    }
}
