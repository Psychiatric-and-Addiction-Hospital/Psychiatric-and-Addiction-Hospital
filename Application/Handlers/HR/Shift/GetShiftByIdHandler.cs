using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using Application.Queries.HR.Shift;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Shift
{
    public class GetShiftByIdHandler : IRequestHandler<GetShiftByIdQuery, BaseResponse<ShiftResponse>>
    {
        private readonly IGetShiftById _service;
        public GetShiftByIdHandler (IGetShiftById service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ShiftResponse>> Handle(GetShiftByIdQuery request, CancellationToken ct)
        {
            return await _service.GetByIdAsync(request.Id, ct);
        }
    }
}
