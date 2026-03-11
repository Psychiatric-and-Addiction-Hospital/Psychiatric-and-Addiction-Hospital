using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Services;
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
    public class GetAllServicesHandler : IRequestHandler<GetAllServicesQuery, BaseResponse<List<ServiceResponse>>>
    {
        private readonly IGetAllServices _service;

        public GetAllServicesHandler(IGetAllServices service)
        {
            _service = service;
        }

        public async Task<BaseResponse<List<ServiceResponse>>> Handle(GetAllServicesQuery request, CancellationToken ct)
        {
            return await _service.GetAllAsync(ct);
        }
    }


}
