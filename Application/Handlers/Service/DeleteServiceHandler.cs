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
    public class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand, BaseResponse<ServiceResponse>>
    {
        private readonly IDeleteService _service;

        public DeleteServiceHandler(IDeleteService service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ServiceResponse>> Handle(DeleteServiceCommand request, CancellationToken ct)
        {
            return await _service.DeleteAsync(request.Id, ct);
        }
    }

}
