using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.ServicesModule;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Depertment
{
    public record GetDepertmentQuery() : IRequest<BaseResponse<List<DepertmentResponse>>>;

}
