using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Service
{
    public class GetServiceByIdHandler : IRequestHandler<GetServiceByIdQuery, BaseResponse<ServiceResponse>>
    {
        private readonly IGetServiceById _service;

        public GetServiceByIdHandler(IGetServiceById service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ServiceResponse>> Handle(GetServiceByIdQuery request, CancellationToken ct)
        {
            return await _service.GetByIdAsync(request.Id, ct);

            //if (result == null)
            //    return ResponseFactory.Fail<ServiceResponse>("Service not found.");

            //return ResponseFactory.Success(result, "Service retrieved successfully.");
        }
    }

}
