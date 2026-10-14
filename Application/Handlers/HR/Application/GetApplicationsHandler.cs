using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.Queries.HR.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Application
{
    public class GetApplicationsHandler : IRequestHandler<GetApplicationsQuery, BaseResponse<PagedResponse<ApplicationResponse>>>
    {
        private readonly IGetApplications _service;

        public GetApplicationsHandler(IGetApplications service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PagedResponse<ApplicationResponse>>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(request, cancellationToken);
        }
    }
}
