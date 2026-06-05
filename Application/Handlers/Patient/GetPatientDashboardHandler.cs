using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class GetPatientDashboardHandler : IRequestHandler<GetPatientDashboardQuery, BaseResponse<PatientDashboardResponse>>
    {
        private readonly IGetPatientDashboard _service;

        public GetPatientDashboardHandler(IGetPatientDashboard service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PatientDashboardResponse>> Handle(GetPatientDashboardQuery request, CancellationToken ct)
        {
            return await _service.GetDashboardAsync(request.PatientId, ct);
        }
    }
}
