using Domain.Entites.HR.Recruitment;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IInterviewInvitationEmailService
    {
        Task SendAsync(Candidate candidate, ApplicationInterview interview, CancellationToken ct);
    }
}
