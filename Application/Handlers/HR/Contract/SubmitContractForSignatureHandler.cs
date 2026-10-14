using Application.Commands.HR.Contract;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class SubmitContractForSignatureHandler : IRequestHandler<SubmitContractForSignatureCommand, BaseResponse<ContractResponse>>
    {
        private readonly ISubmitContractForSignature _service;
        public SubmitContractForSignatureHandler(ISubmitContractForSignature service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ContractResponse>> Handle(SubmitContractForSignatureCommand request, CancellationToken ct)
        {
            return await _service.SubmitContractForSignatureAsync(request.ContractId, ct);
        }
    }
}
