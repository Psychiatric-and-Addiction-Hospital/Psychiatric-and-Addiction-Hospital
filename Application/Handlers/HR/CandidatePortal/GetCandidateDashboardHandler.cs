using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class GetCandidateDashboardHandler : IRequestHandler<GetCandidateDashboardQuery, BaseResponse<CandidateDashboardResponse>>
    {
        private readonly ICandidateDashboard _service;

        public GetCandidateDashboardHandler(ICandidateDashboard service)
        {
            _service = service;
        }

        public async Task<BaseResponse<CandidateDashboardResponse>> Handle(GetCandidateDashboardQuery request, CancellationToken ct)
        {
            return await _service.GetAsync(ct);
        }
    }
}
