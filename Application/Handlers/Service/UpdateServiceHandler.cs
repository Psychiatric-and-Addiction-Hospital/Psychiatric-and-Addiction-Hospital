using Application.Commands.Service;
using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites.ServicesModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Service
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, BaseResponse<ServiceResponse>>
    {
        private readonly IUpdateService _service;

        public UpdateServiceHandler(IUpdateService service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ServiceResponse>> Handle(UpdateServiceCommand request, CancellationToken ct)
        {
            return await _service.UpdateAsync(request.Id, request.Name, request.Description,request.DepartmentId, ct);
        }
    }

}
