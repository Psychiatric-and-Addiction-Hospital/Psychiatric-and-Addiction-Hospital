using Application.Commands.HR.CandidatePortal;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class UpdateMyCandidateProfileHandler : IRequestHandler<UpdateMyCandidateProfileCommand, BaseResponse<CandidateResponse>>
    {
        private readonly IUpdateMyCandidateProfile _service;
        public UpdateMyCandidateProfileHandler(IUpdateMyCandidateProfile service)
        {
            _service = service;
        }
        public async Task<BaseResponse<CandidateResponse>> Handle(UpdateMyCandidateProfileCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.Request, cancellationToken);
        }
    }
}
