using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class GetApplicationStatusHistoryHandler : IRequestHandler<GetApplicationStatusHistoryQuery, BaseResponse<List<ApplicationStatusHistoryResponse>>>
    {
        private readonly IGetApplicationStatusHistory _service;

        public GetApplicationStatusHistoryHandler(IGetApplicationStatusHistory service)
        {
            _service = service;
        }

        public async Task<BaseResponse<List<ApplicationStatusHistoryResponse>>> Handle(GetApplicationStatusHistoryQuery request, CancellationToken ct)
        {
            return await _service.GetAsync(request.ApplicationId, ct);
        }
    }
}
