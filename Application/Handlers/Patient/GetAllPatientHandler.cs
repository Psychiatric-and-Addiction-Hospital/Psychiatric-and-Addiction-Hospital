using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Patient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientQuery, BaseResponse<PagedResponse<PatientProfileResponse>>>
    {
        private readonly IGetAllPatient _service;
        public GetAllPatientHandler(IGetAllPatient service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PagedResponse<PatientProfileResponse>>> Handle(GetAllPatientQuery request, CancellationToken ct)
        {
            return await _service.GetAllAsync(request.request, ct);
        }
    }
}
