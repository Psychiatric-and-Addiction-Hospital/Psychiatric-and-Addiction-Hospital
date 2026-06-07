using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Commands.Patient
{
    public record UploadPatientImageCommand(
        string UserId,
        string ImageUrl
    ) : IRequest<BaseResponse<PatientProfileResponse>>;
}
