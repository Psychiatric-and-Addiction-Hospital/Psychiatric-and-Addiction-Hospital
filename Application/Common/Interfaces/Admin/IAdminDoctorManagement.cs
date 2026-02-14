using Domain.Entites;
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
        Task<List<DoctorApplication>> GetPendingDoctorsAsync(CancellationToken ct);
        Task<List<DoctorApplication>> GetApprovedDoctorsAsync(CancellationToken ct);
        Task<Result<string>> RejectDoctorAsync(Guid applicationId, string reason, CancellationToken ct);
        Task<Result<string>> ApplyForDoctorAsync(Guid ApplicationId, CancellationToken CT);
    }
}
