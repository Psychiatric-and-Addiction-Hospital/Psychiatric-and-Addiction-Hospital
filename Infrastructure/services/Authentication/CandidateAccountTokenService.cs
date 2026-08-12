using Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.services.Authentication
{
    public class CandidateAccountTokenService : ICandidateAccountTokenService
    {
        private readonly IDataProtector _protector;

        public CandidateAccountTokenService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("CandidateAccountSetupToken");
        }

        public string GenerateToken(Guid candidateId, string email)
        {
            var createdAt = DateTime.UtcNow;

            var data = $"{candidateId}|{email}|{createdAt:o}";

            return _protector.Protect(data);
        }

        public bool ValidateToken(string token, Guid candidateId, string email)
        {
            try
            {
                var data = _protector.Unprotect(token);

                var parts = data.Split('|');

                if (parts.Length != 3)
                    return false;

                if (!Guid.TryParse(parts[0], out var tokenCandidateId))
                    return false;

                if (tokenCandidateId != candidateId)
                    return false;

                if (!string.Equals(parts[1], email, StringComparison.OrdinalIgnoreCase))
                    return false;


                if (!DateTime.TryParse(parts[2], out var createdAt))
                    return false;


                if (DateTime.UtcNow > createdAt.AddHours(24))
                    return false;


                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}