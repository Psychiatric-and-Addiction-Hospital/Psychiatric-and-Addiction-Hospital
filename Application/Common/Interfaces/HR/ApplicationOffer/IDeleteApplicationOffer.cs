using Application.Common.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationOffer
{
    public interface IDeleteApplicationOffer
    {
        Task<BaseResponse<bool>> DeleteAsync(Guid OfferId, CancellationToken ct);
    }
}
