using Application.Commands.HR.ApplicationProcess;
using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationProcess
{

    public class createApplicationProcessHandler : IRequestHandler<CreateApplicationProcessCommand, BaseResponse<ApplicationProcessResponse>>
    {
        private readonly ICreateApplicationProcess _createApplicationProcess;
        public createApplicationProcessHandler(ICreateApplicationProcess createApplicationProcess)
        {
            _createApplicationProcess = createApplicationProcess;
        }

        public async Task<BaseResponse<ApplicationProcessResponse>> Handle(CreateApplicationProcessCommand request, CancellationToken ct
            )
        {
            return await _createApplicationProcess.CreateApplicationProcessAsync(request.RecruitmentId, request.CandidateId, ct);
        }
    }
}
