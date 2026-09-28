using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Commands.Doctores.Report
{
    // PUT /api/DoctorReports/{id}  ← الطبيب يعدل تقرير
    public record UpdateReportCommand(
        Guid ReportId,
        string DoctorId,
        string Diagnosis,
        string Notes,
        string TreatmentPlan,
        int ConditionRate,
        string? AttachmentUrl
    ) : IRequest<BaseResponse<SessionNoteResponse>>;
}
