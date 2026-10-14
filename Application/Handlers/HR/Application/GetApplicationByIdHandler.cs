using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.Queries.HR.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Application
{
    public class GetApplicationByIdHandler : IRequestHandler<GetApplicationByIdQuery, BaseResponse<ApplicationResponse>>
    {
        private readonly IGetApplicationById _Service;
        public GetApplicationByIdHandler(IGetApplicationById service)
        {
            _Service = service;
        }
        public async Task<BaseResponse<ApplicationResponse>> Handle(GetApplicationByIdQuery request, CancellationToken ct)
        {
            return await _Service.GetByIdAsync(request.Id, ct);
        }
    }
}
