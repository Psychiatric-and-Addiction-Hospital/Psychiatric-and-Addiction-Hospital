using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Recruitment
{
    public interface IGetAllRecruitment
    {
        Task<BaseResponse<List<RecruitmentResponse>>> GetAllRecruitment(CancellationToken ct);
    }
}
