using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Shift
{
    public interface IUpdateShift
    {
        Task<BaseResponse<ShiftResponse>> UpdateAsync(UpdateShiftRequest request, CancellationToken ct);
    }
}
