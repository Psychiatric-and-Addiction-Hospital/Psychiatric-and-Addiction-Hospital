using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.CandidatePortal
{
    public interface IGetMyApplications
    {
        Task<BaseResponse<List<ApplicationResponse>>> GetAsync(CancellationToken ct);
    }
}
