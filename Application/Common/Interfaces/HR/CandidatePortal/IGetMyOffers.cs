using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface IGetMyOffers
    {
        Task<BaseResponse<List<ApplicationOfferResponse>>> GetAsync(CancellationToken ct);
    }
}
