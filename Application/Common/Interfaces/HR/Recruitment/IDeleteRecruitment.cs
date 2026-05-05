using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Common.Interfaces.HR.Recruitment
{
    public interface IDeleteRecruitment
    {
        Task<BaseResponse<RecruitmentResponse>> DeleteRecruitmentAsync(Guid recruitmentId, CancellationToken ct);
    }
}
