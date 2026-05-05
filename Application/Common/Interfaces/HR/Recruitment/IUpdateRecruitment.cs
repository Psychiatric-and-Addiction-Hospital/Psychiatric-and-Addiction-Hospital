using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Recruitment
{
    public interface IUpdateRecruitment
    {
        Task<BaseResponse<RecruitmentResponse>> UpdateRecruitmentAsync(Guid recruitmentId, string Title, string Description, decimal MinSalary
       , decimal MaxSalary, string ExperienceLevel, CancellationToken ct);
    }
}
