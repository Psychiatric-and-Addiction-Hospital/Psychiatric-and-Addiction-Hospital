using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Candidate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class GetAllCandidateHandler : IRequestHandler<GetAllCandidatesQuery, BaseResponse<List<CandidateResponse>>>
    {
        private readonly IGetAllCandidate _getAllCandidate;
        public GetAllCandidateHandler(IGetAllCandidate getAllCandidate)
        {
            _getAllCandidate = getAllCandidate;
        }
        public async Task<BaseResponse<List<CandidateResponse>>> Handle(GetAllCandidatesQuery request, CancellationToken ct)
        {
            return await _getAllCandidate.GetAllCandidatesAsync(ct);
        }
    }
}
