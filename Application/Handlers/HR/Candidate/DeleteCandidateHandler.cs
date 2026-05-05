using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class DeleteCandidateHandler:IRequestHandler<DeleteCandidateCommand, BaseResponse<CandidateResponse>>
    {
        private readonly IDeleteCandidate _deleteCandidate;
        public DeleteCandidateHandler(IDeleteCandidate deleteCandidate) 
        {
            _deleteCandidate= deleteCandidate;
        }

        public async Task<BaseResponse<CandidateResponse>> Handle(DeleteCandidateCommand request, CancellationToken ct)
        {
            return await _deleteCandidate.DeleteCandidateAsync(request.CandidateId, ct);
        }
    }
}
