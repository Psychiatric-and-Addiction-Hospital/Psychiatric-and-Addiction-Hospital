using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Queries.Patient
{
    public record GetAllPatientQuery(PatientListRequest request) : IRequest<BaseResponse<PagedResponse<PatientProfileResponse>>>;
}
