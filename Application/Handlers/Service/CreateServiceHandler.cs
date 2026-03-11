using Application.Commands.Service;
using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Service
{
    public class CreateServiceHandler : IRequestHandler<CreateServiceCommand, BaseResponse<ServiceResponse>>
    {
        private readonly ICreateService _service;

        public CreateServiceHandler(ICreateService service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ServiceResponse>> Handle(CreateServiceCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request.Name, request.Description, request.DepartmentId, ct);
        }
    }

}
