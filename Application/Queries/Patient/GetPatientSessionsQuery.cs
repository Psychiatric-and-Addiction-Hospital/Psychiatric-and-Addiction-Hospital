using Application.Common.Responses;
using Application.DTOS.Responses.Session;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Queries.Patient
{
    public record GetPatientSessionsQuery(Guid PatientId) : IRequest<BaseResponse<List<SessionSummaryResponse>>>;
}
