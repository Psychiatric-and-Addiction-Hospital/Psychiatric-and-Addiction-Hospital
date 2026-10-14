using Application.Commands.HR.Contract;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class UpdateContractHandler : IRequestHandler<UpdateContractCommand, BaseResponse<ContractResponse>>
    {
        private readonly IUpdateContract _service;
        public UpdateContractHandler(IUpdateContract service)
        {
            _service= service;
        }
        public async Task<BaseResponse<ContractResponse>> Handle(UpdateContractCommand request, CancellationToken ct)
        {
            return await _service.UpdateAsync(request.request, ct);
        }
    }
}
