using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using Application.DTOS.Responses.HR.Contract;
using MediatR;

namespace Application.Commands.HR.Contract
{
    public record CreateContractCommand(CreateContractRequest request) : IRequest<BaseResponse<ContractResponse>>;
}
