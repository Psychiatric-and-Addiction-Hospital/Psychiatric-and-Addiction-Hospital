using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Depertment
{
    public interface IUpdateDepartment
    {
        Task<BaseResponse<DepertmentResponse>> UpdateAsync(Guid id, string name, string description, CancellationToken ct);
    }
}
