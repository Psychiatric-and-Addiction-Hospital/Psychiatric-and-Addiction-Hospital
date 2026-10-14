using Application.Commands.HR.Shift;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Shift
{
    public class UpdateShiftHandler : IRequestHandler<UpdateShiftCommand, BaseResponse<ShiftResponse>>
    {
        private readonly IUpdateShift _service;

        public UpdateShiftHandler(IUpdateShift service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ShiftResponse>> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.request, cancellationToken);
        }
    }
}
