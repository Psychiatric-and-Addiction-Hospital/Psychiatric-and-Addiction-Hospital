using Application.Commands.HR.Contract;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class CreateContractHandler : IRequestHandler<CreateContractCommand, BaseResponse<ContractResponse>>
    {
        private readonly ICreateContract _service;

        public CreateContractHandler(ICreateContract service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ContractResponse>> Handle(CreateContractCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request.request, ct);
        }
    }
}
