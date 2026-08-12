using Application.Common.Responses;
using Application.DTOS.Responses.HR.Manager;
using MediatR;
using System;

namespace Application.Commands.HR.Manager
{
    public record RemoveDepartmentManagerCommand(Guid DepartmentId) : IRequest<BaseResponse<DepartmentManagerResponse>>;
}
