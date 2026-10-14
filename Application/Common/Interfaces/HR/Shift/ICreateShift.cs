using Application.Commands.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Shift
{
    public interface ICreateShift
    {
        Task<BaseResponse<ShiftResponse>> CreateAsync(
           CreateShiftRequest request, CancellationToken ct);
    }
}
