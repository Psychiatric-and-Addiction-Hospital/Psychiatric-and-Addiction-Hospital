using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, BaseResponse<CandidateResponse>>
    {
        private readonly IUpdateCandidate _service;
        public UpdateCandidateHandler(IUpdateCandidate service)
        {
            
            _service = service;
        }

        public async Task<BaseResponse<CandidateResponse>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.Request, cancellationToken);
        }
    }
}
