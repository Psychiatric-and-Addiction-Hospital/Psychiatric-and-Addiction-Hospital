using Application.Commands.HR.CandidatePortal;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class WithdrawApplicationHandler : IRequestHandler<WithdrawApplicationCommand, BaseResponse<ApplicationResponse>>
    {
        private readonly IWithdrawApplication _service;
        public WithdrawApplicationHandler(IWithdrawApplication service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationResponse>> Handle(WithdrawApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _service.WithdrawAsync(request.Id, cancellationToken);
        }
    }
}
