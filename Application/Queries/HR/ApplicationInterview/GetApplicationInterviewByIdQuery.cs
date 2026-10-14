using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;
using System;

namespace Application.Queries.HR.ApplicationInterview
{
    public record GetApplicationInterviewByIdQuery(Guid Id) : IRequest<BaseResponse<ApplicationInterviewResponse>>;

}
