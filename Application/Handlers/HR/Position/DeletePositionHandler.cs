using Application.Commands.HR.Position;
using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Position
{
    public class DeletePositionHandler : IRequestHandler<DeletePositionCommand, BaseResponse<PositionResponse>>
    {
        private readonly IDeletePosition _position;
        public DeletePositionHandler(IDeletePosition position)
        {
            _position = position;
        }
        public Task<BaseResponse<PositionResponse>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            return _position.DeletePositionAsync(request.Id, cancellationToken);
        }
    }
}
