using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Depertment
{
    public interface ICreateDepartment
    {
        Task<BaseResponse<DepertmentResponse>> CreateAsync(Guid MangerId, string name, string description, CancellationToken ct);

    }
}
