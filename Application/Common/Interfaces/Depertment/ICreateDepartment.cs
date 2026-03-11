
using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Depertment
{
    public interface ICreateDepartment
    {
        Task<BaseResponse<DepertmentResponse>> CreateAsync(string name, string description, CancellationToken ct);
    }
}
