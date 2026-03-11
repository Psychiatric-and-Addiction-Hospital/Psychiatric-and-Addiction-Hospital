using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Depertment
{
    public interface IGetDepertments
    {
        Task<BaseResponse<List<DepertmentResponse>>> GetAllDepertment(CancellationToken ct);
    }
}
