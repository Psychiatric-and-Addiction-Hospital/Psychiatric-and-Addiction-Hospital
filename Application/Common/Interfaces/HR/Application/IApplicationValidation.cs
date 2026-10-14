using Application.Common.Responses;
using Application.DTOS.Request.HR.Application;
using Domain.Enums.HR;
using System;
using System.Threading;
using System.Threading.Tasks;
using applicationEntity = Domain.Entites.HR.Recruitment.Application;

namespace Application.Common.Interfaces.HR.Application
{
    public interface IApplicationValidation
    {
        Task<BaseResponse<bool>> ValidateApplyAsync(CreateApplicationRequest request,
        CancellationToken ct);

        Task<BaseResponse<applicationEntity>> ValidateStatusUpdateAsync(
            Guid applicationId,
            CancellationToken ct);

        Task<BaseResponse<applicationEntity>> ValidateStatusTransitionAsync(
          Guid applicationId,
          ApplicationStatus newStatus,
          CancellationToken ct);

        Task<BaseResponse<applicationEntity>> ValidateDeleteAsync(
            Guid applicationId,
            CancellationToken ct);
    }
}
