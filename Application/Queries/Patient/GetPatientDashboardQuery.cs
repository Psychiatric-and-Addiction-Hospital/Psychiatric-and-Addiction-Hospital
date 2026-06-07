using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Queries.Patient
{
    public record GetPatientDashboardQuery(string PatientId) : IRequest<BaseResponse<PatientDashboardResponse>>;
}
