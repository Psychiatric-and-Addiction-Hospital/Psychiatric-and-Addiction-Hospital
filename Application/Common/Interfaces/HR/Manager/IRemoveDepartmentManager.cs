using Application.Common.Responses;
using Application.DTOS.Responses.HR.Manager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Manager
{
    public interface IRemoveDepartmentManager
    {
        Task<BaseResponse<DepartmentManagerResponse>> RemoveAsync(Guid departmentId, CancellationToken ct);
    }
}
