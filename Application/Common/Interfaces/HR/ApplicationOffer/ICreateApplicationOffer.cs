using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationOffer
{
    public interface ICreateApplicationOffer
    {
        Task<BaseResponse<ApplicationOfferResponse>> CreateApplicationOfferAsync(
            Guid applicationProcessId, decimal offeredSalary, DateTime expiryDate,CancellationToken ct);
    }
}
