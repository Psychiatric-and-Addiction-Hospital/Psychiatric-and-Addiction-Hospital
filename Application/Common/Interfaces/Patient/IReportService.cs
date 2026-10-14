using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IReportService
    {
        Task<BaseResponse<List<SessionNoteResponse>>> GetPatientReportsAsync(string patientId, CancellationToken ct);
        Task<BaseResponse<SessionNoteResponse>> GetReportByIdAsync(Guid reportId, CancellationToken ct);
        Task<BaseResponse<List<SessionNoteResponse>>> GetSessionReportsAsync(Guid sessionId, CancellationToken ct);
        Task<BaseResponse<SessionNoteResponse>> UpdateReportAsync(Guid reportId, string doctorId, string diagnosis, string notes, string treatmentPlan, int conditionRate, string? attachmentUrl, CancellationToken ct);
    }
}
