using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Position;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Position
{
    public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, BaseResponse<PagedResponse<PositionResponse>>>
    {
        private readonly IGetPositions _position;

        public GetPositionsHandler(IGetPositions position)
        {
            _position = position;
        }

        public async Task<BaseResponse<PagedResponse<PositionResponse>>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            return await _position.GetAllPositionsAsync(request.request, cancellationToken);
        }
    }
}
