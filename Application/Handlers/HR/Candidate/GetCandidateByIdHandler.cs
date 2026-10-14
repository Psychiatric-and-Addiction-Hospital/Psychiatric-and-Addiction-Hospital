using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class GetCandidateByIdHandler : IRequestHandler<GetCandidateByIdQuery, BaseResponse<CandidateResponse>>
    {
        private readonly IGetCandidateById _service;
        public GetCandidateByIdHandler(IGetCandidateById service)
        {
            _service = service;
        }
        public async Task<BaseResponse<CandidateResponse>> Handle(GetCandidateByIdQuery request, CancellationToken ct)
        {
            return await _service.GetByIdAsync(request.Id, ct);
        }
    }
}
