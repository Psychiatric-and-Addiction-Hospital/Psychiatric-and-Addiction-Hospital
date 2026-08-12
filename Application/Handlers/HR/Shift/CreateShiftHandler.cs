using Application.Commands.HR.Shift;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Handlers.HR.Shift
{
    public class CreateShiftHandler : IRequestHandler<CreateShiftCommand, BaseResponse<ShiftResponse>>
    {
        private readonly ICreateShift _service;

        public CreateShiftHandler(ICreateShift service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ShiftResponse>> Handle(
            CreateShiftCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(
               request,
                cancellationToken);
        }
    }
}
