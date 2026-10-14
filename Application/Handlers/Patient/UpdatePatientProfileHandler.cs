using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class UpdatePatientProfileHandler : IRequestHandler<UpdatePatientProfileCommand, BaseResponse<PatientProfileResponse>>
    {
        private readonly IUpdatePatientProfile _service;

        public UpdatePatientProfileHandler(IUpdatePatientProfile service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PatientProfileResponse>> Handle(UpdatePatientProfileCommand request, CancellationToken ct)
        {
            return await _service.UpdateAsync(request.request, ct);
        }
    }
}
