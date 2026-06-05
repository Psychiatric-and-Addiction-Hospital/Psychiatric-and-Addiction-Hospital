using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Commands.Patient
{
    public record AddSessionNoteCommand(
        string DoctorId,
        string PatientId,
        Guid SessionId,
        string Diagnosis,
        string Notes,
        string TreatmentPlan,
        int ConditionRate,
        string? AttachmentUrl
    ) : IRequest<BaseResponse<SessionNoteResponse>>;
}
