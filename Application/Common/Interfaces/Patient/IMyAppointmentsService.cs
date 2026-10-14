using Application.Commands.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses.Session;
using Application.DTOs.Responses.Sessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IMyAppointmentsService
    {
        Task<BaseResponse<List<SessionSummaryResponse>>> GetAppointmentsAsync(string patientId, string status, CancellationToken ct);
        Task<BaseResponse<SessionDetailsResponse>> GetAppointmentDetailsAsync(Guid sessionId, string patientId, CancellationToken ct);
        Task<BaseResponse<Unit>> CancelAppointmentAsync(CancelAppointmentCommand command, CancellationToken ct);
        Task<BaseResponse<Unit>> CancelBookingRequestAsync(CancelBookingRequestCommand command, CancellationToken ct);
    }

    public interface ISettingsService
    {
        Task<BaseResponse<Unit>> ChangePasswordAsync(ChangePasswordCommand command, CancellationToken ct);
        Task<BaseResponse<PreferencesResponse>> GetPreferencesAsync(string userId, CancellationToken ct);
        Task<BaseResponse<Unit>> UpdatePreferencesAsync(UpdatePreferencesCommand command, CancellationToken ct);
    }
}
