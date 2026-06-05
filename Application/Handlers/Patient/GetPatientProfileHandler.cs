using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class GetPatientProfileHandler : IRequestHandler<GetPatientProfileQuery, BaseResponse<PatientProfileResponse>>
    {
        private readonly IGetPatientProfile _service;

        public GetPatientProfileHandler(IGetPatientProfile service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PatientProfileResponse>> Handle(GetPatientProfileQuery request, CancellationToken ct)
        {
            return await _service.GetProfileAsync(request.UserId, ct);
        }
    }
}
