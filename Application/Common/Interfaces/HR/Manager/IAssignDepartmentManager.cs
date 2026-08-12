using Application.Common.Responses;
using Application.DTOS.Request.HR.manager;
using Application.DTOS.Responses.HR.Manager;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Manager
{
    public interface IAssignDepartmentManager
    {
        Task<BaseResponse<DepartmentManagerResponse>> AssignAsync(AssignDepartmentManagerRequest request,CancellationToken ct);
    }
}
