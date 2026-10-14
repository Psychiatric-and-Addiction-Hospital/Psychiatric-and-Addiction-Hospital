using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.DTOS.Responses.HR.ApplicationOffer;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationOffer
{
    public interface IUpdateApplicationOffer
    {
        Task<BaseResponse<ApplicationOfferResponse>> UpdateAsync(UpdateApplicationOfferRequest request, CancellationToken ct);
    }
}
