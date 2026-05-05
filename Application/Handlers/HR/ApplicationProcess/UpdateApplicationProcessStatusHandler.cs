using Application.Commands.HR.ApplicationProcess;
using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationProcess
{
    public class UpdateApplicationProcessStatusHandler : IRequestHandler<UpdateApplicationProcessStatusCommand, BaseResponse<ApplicationProcessResponse>>
    {
        private readonly IUpdateApplicationProcessStatus _updateStatus;
        public UpdateApplicationProcessStatusHandler (IUpdateApplicationProcessStatus updateStatus)
        {
            _updateStatus = updateStatus;
        }
        public async Task<BaseResponse<ApplicationProcessResponse>> Handle(UpdateApplicationProcessStatusCommand request, CancellationToken ct)
        {
            return await _updateStatus.UpdateApplicationProcessStatus(request.ApplicationProcessId,request.Status,ct);
        }
    }
}
