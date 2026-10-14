using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface ICandidateAccountEmailService
    {
        Task SendAsync(string email, string fullName, string accountSetupUrl, CancellationToken ct);
    }
}
