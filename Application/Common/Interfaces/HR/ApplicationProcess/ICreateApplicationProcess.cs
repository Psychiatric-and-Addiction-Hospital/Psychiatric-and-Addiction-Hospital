using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Common.Interfaces.HR.ApplicationProcess
{
    public interface ICreateApplicationProcess
    {
        Task<BaseResponse<ApplicationProcessResponse>> CreateApplicationProcessAsync(
            Guid CandidateId, Guid RecruitmentId, CancellationToken ct);
    }
}
