using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Queries.HR.Contract
{
    public record GetContractByIdQuery(Guid Id) : IRequest<BaseResponse<ContractResponse>>;
   
}
