using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.ApplicationProcesses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationProcess
{
    public class GetAllApplicationProcessesHandler:IRequestHandler<GetAllApplicationProcessesQuery, BaseResponse<List<ApplicationProcessResponse>>>
    {
        private readonly IGetAllApplicationProcesses _getAllApplicationProcesses;
        public GetAllApplicationProcessesHandler(IGetAllApplicationProcesses getAllApplicationProcesses)
        {
            _getAllApplicationProcesses = getAllApplicationProcesses;
        }

        public async Task<BaseResponse<List<ApplicationProcessResponse>>> Handle(GetAllApplicationProcessesQuery request, CancellationToken ct)
        {
           return await _getAllApplicationProcesses.GetAllApplicationProcessesAsync(ct);
        }
    }
}
