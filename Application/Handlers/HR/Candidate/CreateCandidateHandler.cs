using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Handlers.HR.Candidate
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, BaseResponse<CandidateResponse>>
    {
        private readonly ICreateCandidate _service;

        public CreateCandidateHandler(ICreateCandidate service)
        {
            _service = service;
        }

        public async Task<BaseResponse<CandidateResponse>> Handle(CreateCandidateCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request.Request, ct);
        }
    }
}
