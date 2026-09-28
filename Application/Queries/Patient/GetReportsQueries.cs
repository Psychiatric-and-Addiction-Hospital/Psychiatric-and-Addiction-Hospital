using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Queries.Patient
{
    // GET /api/Reports  ← كل تقارير المريض
    public record GetPatientReportsQuery(string PatientId)
        : IRequest<BaseResponse<List<SessionNoteResponse>>>;

    // GET /api/Reports/{id}  ← تقرير واحد بالتفصيل
    public record GetReportByIdQuery(Guid ReportId)
        : IRequest<BaseResponse<SessionNoteResponse>>;

    // GET /api/Reports/session/{sessionId}  ← تقارير جلسة معينة
    public record GetSessionReportsQuery(Guid SessionId)
        : IRequest<BaseResponse<List<SessionNoteResponse>>>;
}
