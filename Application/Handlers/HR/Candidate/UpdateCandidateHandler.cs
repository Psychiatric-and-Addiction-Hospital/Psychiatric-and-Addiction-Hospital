using Application.Commands.HR.Candidate;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class UpdateCandidateHandler:IRequestHandler<UpdateCandidateCommand, BaseResponse<CandidateResponse>>
    {
        private readonly IUpdateCandidate _updateCandidate;
        public UpdateCandidateHandler(IUpdateCandidate updateCandidate)
        {
            _updateCandidate = updateCandidate;
        }

        public async Task<BaseResponse<CandidateResponse>> Handle(UpdateCandidateCommand request, CancellationToken ct)
        {
          return await _updateCandidate.UpdateCandidateAsync(request.CandidateId, request.FullName, request.Email, request.Phone, request.ResumeUrl, ct);
        }
    }
}
