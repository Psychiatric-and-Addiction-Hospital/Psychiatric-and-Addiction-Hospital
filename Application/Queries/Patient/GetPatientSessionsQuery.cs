using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Patient
{
    public record GetPatientSessionsQuery(string PatientId) : IRequest<BaseResponse<List<SessionSummaryResponse>>>;
}
