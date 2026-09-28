using Application.Common.Responses;
using Application.DTOS.Responses.Report;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Doctores
{
    public record CreateReportCommand(
        string DoctorId,
        string PatientId,
        Guid SessionId,
        string Diagnosis,
        string Notes,
        string TreatmentPlan,
        int ConditionRate,
        string? AttachmentUrl,
        DateTime CreatedAt
    ) : IRequest<BaseResponse<ReportResponse>>;

}
