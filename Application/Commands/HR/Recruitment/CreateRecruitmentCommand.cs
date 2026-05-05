using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.HR.Recruitment
{
    public record CreateRecruitmentCommand(string Title, string Description, decimal MinSalary, decimal MaxSalary,
        string ExperienceLevel, Guid DepartmentId, Guid HiringManagerId)
        : IRequest<BaseResponse<RecruitmentResponse>>;


}
