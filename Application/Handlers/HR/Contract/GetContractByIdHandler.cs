using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public record GetContractByIdHandler : IRequestHandler<GetContractByIdQuery, BaseResponse<ContractResponse>>
    {
        private readonly IGetContractById _getContractById;
        public GetContractByIdHandler(IGetContractById getContractById)
        {
            _getContractById = getContractById;
        }
        public async Task<BaseResponse<ContractResponse>> Handle(GetContractByIdQuery request, CancellationToken ct)
        {
            return await _getContractById.GetContractByIdAsync(request.Id, ct);
        }
    }
}
