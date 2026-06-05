using Application.Commands.Patient;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Patient
{
    public class UploadPatientImageHandler : IRequestHandler<UploadPatientImageCommand, BaseResponse<PatientProfileResponse>>
    {
        private readonly IUploadPatientImage _service;

        public UploadPatientImageHandler(IUploadPatientImage service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PatientProfileResponse>> Handle(UploadPatientImageCommand request, CancellationToken ct)
        {
            return await _service.UploadImageAsync(request.UserId, request.ImageUrl, ct);
        }
    }
}
