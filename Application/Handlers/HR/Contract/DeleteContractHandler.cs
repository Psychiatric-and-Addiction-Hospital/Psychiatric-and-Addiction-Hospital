using Application.Commands.HR.Contract;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class DeleteContractHandler : IRequestHandler<DeleteContractCommand, BaseResponse<ContractResponse>>
    {
        private readonly IDeleteContract _deletecontract;
        public DeleteContractHandler (IDeleteContract deletecontract)
        {
            _deletecontract = deletecontract;
        }
        public async Task<BaseResponse<ContractResponse>> Handle(DeleteContractCommand request, CancellationToken ct)
        {
            return await _deletecontract.DeleteContractAsync(request.ContractId,ct);
        }
    }
}
