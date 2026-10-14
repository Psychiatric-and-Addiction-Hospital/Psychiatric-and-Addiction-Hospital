using Application.Commands.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Shift
{
    public interface IDeleteShift
    {
        Task<BaseResponse<ShiftResponse>> DeleteAsync(
           DeleteShiftCommand request,
           CancellationToken ct);
    }
}
