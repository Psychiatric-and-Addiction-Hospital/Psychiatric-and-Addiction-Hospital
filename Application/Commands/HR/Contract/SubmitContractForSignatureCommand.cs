using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using MediatR;
using System;

namespace Application.Commands.HR.Contract
{
    public record SubmitContractForSignatureCommand(Guid ContractId) : IRequest<BaseResponse<ContractResponse>>;
}
