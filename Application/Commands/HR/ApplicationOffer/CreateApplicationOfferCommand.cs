using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;


namespace Application.Commands.HR.ApplicationOffer
{
    public record CreateApplicationOfferCommand(Guid ApplicationProcessId, decimal OfferedSalary, DateTime ExpiryDate)
        : IRequest<BaseResponse<ApplicationOfferResponse>>;

}
