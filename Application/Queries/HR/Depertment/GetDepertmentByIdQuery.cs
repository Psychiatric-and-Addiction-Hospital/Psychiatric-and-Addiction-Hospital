using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;


namespace Application.Queries.Depertments
{
    public record GetDepertmentByIdQuery(Guid Id
        ) : IRequest<BaseResponse<DepertmentResponse>>;
    
}
