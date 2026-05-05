using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Contract
{
    public record DeleteContractCommand(Guid ContractId) : IRequest<BaseResponse<ContractResponse>>;

}
