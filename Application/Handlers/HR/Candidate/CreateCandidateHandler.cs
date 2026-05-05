using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, BaseResponse<CandidateResponse>>
    {
        private readonly ICreateCandidate _createCandidate;
        public CreateCandidateHandler(ICreateCandidate createCandidate)
        {
            _createCandidate = createCandidate;
        }
        public async Task<BaseResponse<CandidateResponse>> Handle(CreateCandidateCommand request, CancellationToken ct)
        {
            return await _createCandidate.CreateCandidateAsync(request.FullName, request.Email, request.Phone, request.ResumeUrl,ct);
        }
    }
}
