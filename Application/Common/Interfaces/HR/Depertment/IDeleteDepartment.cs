using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Depertment
{
    public interface IDeleteDepartment
    {
        Task<BaseResponse<DepartmentResponse>> DeleteDepartmentAsync(Guid Id,CancellationToken ct);
    }
}
