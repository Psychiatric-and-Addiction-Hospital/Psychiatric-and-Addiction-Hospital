using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;


namespace Application.Queries.HR.Recruitment
{
    public record GetRecruitmentQuery() : IRequest<BaseResponse<List<RecruitmentResponse>>>;


}
