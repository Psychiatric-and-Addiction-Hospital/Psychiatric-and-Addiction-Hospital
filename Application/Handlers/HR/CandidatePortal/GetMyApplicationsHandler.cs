using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class GetMyApplicationsHandler : IRequestHandler<GetMyApplicationsQuery, BaseResponse<List<ApplicationResponse>>>
    {
        private readonly IGetMyApplications _service;
        public GetMyApplicationsHandler(IGetMyApplications service)
        {
            _service = service;
        }

        public async Task<BaseResponse<List<ApplicationResponse>>> Handle(GetMyApplicationsQuery request, CancellationToken ct)
        {
            return await _service.GetAsync(ct);
        }
    }
}
