using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites.DoctorsModule;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Admin
{
    public interface IAdminDoctorManagement
    {
        Task<BaseResponse<List<PendingDoctorApplicationResponse>>> GetPendingDoctorsAsync(CancellationToken ct);
        Task<BaseResponse<List<ApprovedDoctorApplicationRespons>>> GetApprovedDoctorsAsync(CancellationToken ct);
        Task<BaseResponse<string>> RejectDoctorAsync(Guid applicationId, string reason, CancellationToken ct);
        Task<BaseResponse<string>> ApplyForDoctorAsync(Guid ApplicationId, Guid DepartmentId, CancellationToken CT);
    }
}
