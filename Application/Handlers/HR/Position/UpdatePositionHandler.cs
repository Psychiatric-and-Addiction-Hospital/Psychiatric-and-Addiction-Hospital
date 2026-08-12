using Application.Commands.HR.Position;
using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Position
{
    public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand, BaseResponse<PositionResponse>>
    {
        private readonly IUpdatePosition _position;

        public UpdatePositionHandler(IUpdatePosition position)
        {
            _position = position;
        }

        public async Task<BaseResponse<PositionResponse>> Handle(UpdatePositionCommand request, CancellationToken ct)
        {
            return await _position.UpdatePositionAsync(request.request,ct);
        }
    }
}
