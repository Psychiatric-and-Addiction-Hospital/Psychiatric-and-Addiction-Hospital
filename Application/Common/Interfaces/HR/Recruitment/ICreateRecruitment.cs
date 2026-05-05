using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Recruitment
{
    public interface ICreateRecruitment
    {
        Task<BaseResponse<RecruitmentResponse>> CreateRecruitmentAsync(string Title, string Description, decimal MinSalary
       , decimal MaxSalary, string ExperienceLevel, Guid DepartmentId, Guid HiringManagerId, CancellationToken ct);
    }
}
