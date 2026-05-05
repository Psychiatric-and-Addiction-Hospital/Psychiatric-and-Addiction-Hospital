using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;


namespace Application.Commands.HR.Recruitment
{
    public record UpdateRecruitmentCommand(Guid recruitmentId, string Title, string Description, decimal MinSalary
       , decimal MaxSalary, string ExperienceLevel) : IRequest<BaseResponse<RecruitmentResponse>>;

}
