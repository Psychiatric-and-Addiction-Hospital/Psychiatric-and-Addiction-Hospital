using Application.Commands.HR.CandidatePortal;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class CreateCandidateAccountHandler : IRequestHandler<CreateCandidateAccountCommand, BaseResponse<CandidateAccountResponse>>
    {
        private readonly ICreateCandidateAccount _service;
        public CreateCandidateAccountHandler(ICreateCandidateAccount service)
        {
            _service = service;
        }
        public async Task<BaseResponse<CandidateAccountResponse>> Handle(CreateCandidateAccountCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(request.Request, cancellationToken);
        }
    }
}
