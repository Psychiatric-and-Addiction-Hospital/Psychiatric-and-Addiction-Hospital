using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class DeleteCandidateHandler : IRequestHandler<DeleteCandidateCommand, BaseResponse<CandidateResponse>>
    {
        private readonly IDeleteCandidate _service;
        public DeleteCandidateHandler(IDeleteCandidate service)
        {
            _service = service;
        }
        public async Task<BaseResponse<CandidateResponse>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.CandidateId, cancellationToken);
        }
    }
}
