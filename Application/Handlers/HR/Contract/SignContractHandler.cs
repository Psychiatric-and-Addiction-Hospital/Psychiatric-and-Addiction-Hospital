using Application.Commands.HR.CandidatePortal;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class SignContractHandler : IRequestHandler<SignContractCommand, BaseResponse<ContractResponse>>
    {
        private readonly ISignContract _service;
        public SignContractHandler(ISignContract service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ContractResponse>> Handle(SignContractCommand request, CancellationToken ct)
        {
            return await _service.SignContractAsync(request.ContractId, ct);
        }
    }
}
