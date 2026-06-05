using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class GetSessionDetailsHandler : IRequestHandler<GetSessionDetailsQuery, BaseResponse<SessionDetailResponse>>
    {
        private readonly IGetSessionDetails _service;

        public GetSessionDetailsHandler(IGetSessionDetails service)
        {
            _service = service;
        }

        public async Task<BaseResponse<SessionDetailResponse>> Handle(GetSessionDetailsQuery request, CancellationToken ct)
        {
            return await _service.GetDetailsAsync(request.SessionId, ct);
        }
    }
}
