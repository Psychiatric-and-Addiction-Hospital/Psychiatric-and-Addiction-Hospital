using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System;

namespace Application.Queries.HR.Application
{
    public record GetApplicationByIdQuery(Guid Id)
       : IRequest<BaseResponse<ApplicationResponse>>;
}
