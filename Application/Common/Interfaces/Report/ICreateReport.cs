using Application.Commands;
using Application.Common.Responses;
using Application.DTOS.Responses.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Report
{
    public interface ICreateReport
    {
        Task<BaseResponse<ReportResponse>> CreateReportAsync(string patientId, Guid sessionId, string doctorId, string diagnosis, string notes, string treatmentPlan, int conditionRate, string? attachmentUrl, DateTime createdAt, CancellationToken ct);
    }
}
