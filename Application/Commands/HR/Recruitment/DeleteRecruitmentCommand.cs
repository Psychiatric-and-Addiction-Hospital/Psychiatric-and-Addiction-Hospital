using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;


namespace Application.Commands.HR.Recruitment
{
    public record DeleteRecruitmentCommand(Guid RecruitmentId) : IRequest<BaseResponse<RecruitmentResponse>>;

}
