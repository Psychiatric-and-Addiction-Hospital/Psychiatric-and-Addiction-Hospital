using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationProcess
{
    public interface IGetAllApplicationProcesses
    {
        Task<BaseResponse<List<ApplicationProcessResponse>>> GetAllApplicationProcessesAsync(CancellationToken ct);
    }
}
