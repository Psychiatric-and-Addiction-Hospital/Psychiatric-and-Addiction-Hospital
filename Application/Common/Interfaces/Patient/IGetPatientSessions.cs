using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IGetPatientSessions
    {
        Task<BaseResponse<List<SessionSummaryResponse>>> GetSessionsAsync(Guid patientId, CancellationToken ct);
    }
}
