using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class GetCandidatesHandler:IRequestHandler<GetCandidatesQuery,BaseResponse<PagedResponse<CandidateResponse>>>
    {
        private readonly IGetCandidates _getCandidates;
        public GetCandidatesHandler(IGetCandidates getCandidates)
        {
            _getCandidates = getCandidates;
        }

        public async Task<BaseResponse<PagedResponse<CandidateResponse>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _getCandidates.GetAllAsync(request.Request, cancellationToken);
        }
    }
}
