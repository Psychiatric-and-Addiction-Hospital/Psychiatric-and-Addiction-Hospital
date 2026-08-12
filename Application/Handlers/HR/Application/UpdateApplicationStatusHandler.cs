using Application.Commands.HR.Application;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Application
{
    public class UpdateApplicationStatusHandler:IRequestHandler<UpdateApplicationStatusCommand, BaseResponse<ApplicationResponse>>
    {
        private readonly IUpdateApplicationStatus _service;
        public UpdateApplicationStatusHandler(IUpdateApplicationStatus service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationResponse>> Handle(UpdateApplicationStatusCommand request, CancellationToken ct)
        {
            return await _service.UpdateAsync(request, ct);
        }
    }
}
