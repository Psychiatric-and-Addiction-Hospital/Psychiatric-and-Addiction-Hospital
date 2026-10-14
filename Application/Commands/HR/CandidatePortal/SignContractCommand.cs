using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using MediatR;
using System;

namespace Application.Commands.HR.CandidatePortal
{
    public record SignContractCommand(Guid ContractId) : IRequest<BaseResponse<ContractResponse>>;
}
