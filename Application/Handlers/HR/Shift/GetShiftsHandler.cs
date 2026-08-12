using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using Application.Queries.HR.Shift;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Shift
{
    public class GetShiftsHandler : IRequestHandler<GetAllShiftsQuery, BaseResponse<PagedResponse<ShiftResponse>>>
    {

        private readonly IGetShifts _service;
        public GetShiftsHandler(IGetShifts service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PagedResponse<ShiftResponse>>> Handle(GetAllShiftsQuery request, CancellationToken ct)
        {
            return await _service.GetAllAsync(request.request, ct);
        }
    }
}
