using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Position;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Position
{
    public class GetPositionByIdHandler : IRequestHandler<GetPositionByIdQuery, BaseResponse<PositionResponse>>
    {
        private readonly IGetPositionById _position;

        public GetPositionByIdHandler(IGetPositionById position)
        {
            _position = position;
        }

        public async Task<BaseResponse<PositionResponse>> Handle(
            GetPositionByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _position.GetByIdAsync(
                request.Id,
                cancellationToken);
        }
    }
}
