using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Depertment
{
    public interface IGetDepartmentById
    {
        Task<BaseResponse<DepertmentResponse>> GetDepertmentById(Guid id, CancellationToken ct);
    }
}
