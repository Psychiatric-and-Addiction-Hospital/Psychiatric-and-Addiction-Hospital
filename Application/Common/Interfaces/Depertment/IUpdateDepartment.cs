using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Depertment
{
    public interface IUpdateDepartment
    {
        Task<BaseResponse<DepertmentResponse>> UpdateAsync(Guid id, string name, string description, CancellationToken ct);
    }
}
