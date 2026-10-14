using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class GetMyCandidateProfileHandler : IRequestHandler<GetMyCandidateProfileQuery, BaseResponse<CandidateResponse>>
    {
        private readonly IGetMyCandidateProfile _service;

        public GetMyCandidateProfileHandler(IGetMyCandidateProfile service)
        {
            _service = service;
        }

        public async Task<BaseResponse<CandidateResponse>> Handle(GetMyCandidateProfileQuery request, CancellationToken ct)
        {
            return await _service.GetAsync(ct);
        }
    }
}
