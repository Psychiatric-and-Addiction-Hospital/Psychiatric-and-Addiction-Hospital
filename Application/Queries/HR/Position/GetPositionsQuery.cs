using Application.Common.Responses;
using Application.DTOS.Request.HR.Position;
using Application.DTOS.Responses.HR;
using MediatR;

namespace Application.Queries.HR.Position
{
    public record GetPositionsQuery(PositionListRequest request) : IRequest<BaseResponse<PagedResponse<PositionResponse>>>;
}
