using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Depertment
{
    public interface IGetDepartmentById
    {
        Task<BaseResponse<DepartmentResponse>> GetDepartmentById(Guid id, CancellationToken ct);
    }
}
