using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Queries.Patient
{
    public record GetPatientProfileQuery(string UserId) : IRequest<BaseResponse<PatientProfileResponse>>;
}
