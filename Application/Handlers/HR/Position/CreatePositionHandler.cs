using Application.Commands.HR.Position;
using Application.Common.Interfaces.HR.Position;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Position
{
    public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, BaseResponse<PositionResponse>>
    {
        private readonly ICreatePosition _position;

        public CreatePositionHandler(ICreatePosition position)
        {
            _position = position;
        }

        public async Task<BaseResponse<PositionResponse>> Handle(CreatePositionCommand request, CancellationToken ct)
        {
            return await _position.CreatePositionAsync(request.request, ct);
        }
    }
}
        