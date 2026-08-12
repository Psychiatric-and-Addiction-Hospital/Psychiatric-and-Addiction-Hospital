using Domain.Entites.HR.Recruitment;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJobOfferEmailService
    {
        Task SendAsync(Candidate candidate, ApplicationOffer offer, CancellationToken ct);
    }
}
