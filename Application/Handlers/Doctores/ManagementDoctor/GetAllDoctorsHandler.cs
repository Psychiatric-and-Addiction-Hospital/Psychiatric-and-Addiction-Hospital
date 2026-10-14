using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Doctor.ManagementDoctor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Doctores.ManagementDoctor
{
    public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctorsQuery, BaseResponse<PagedResponse<DoctorProfileResponse>>>
    {
        private readonly IGetAllDoctors _service;
        public GetAllDoctorsHandler(IGetAllDoctors service)
        {
            _service = service;
        }
        public async Task<BaseResponse<PagedResponse<DoctorProfileResponse>>> Handle(GetAllDoctorsQuery request, CancellationToken ct)
        {
            return await _service.GetAllDoctorsAsync(request.request, ct);
        }
    }
}
