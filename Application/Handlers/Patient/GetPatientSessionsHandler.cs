using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class GetPatientSessionsHandler : IRequestHandler<GetPatientSessionsQuery, BaseResponse<List<SessionSummaryResponse>>>
    {
        private readonly IGetPatientSessions _service;

        public GetPatientSessionsHandler(IGetPatientSessions service)
        {
            _service = service;
        }

        public async Task<BaseResponse<List<SessionSummaryResponse>>> Handle(GetPatientSessionsQuery request, CancellationToken ct)
        {
            return await _service.GetSessionsAsync(request.PatientId, ct);
        }
    }
}
