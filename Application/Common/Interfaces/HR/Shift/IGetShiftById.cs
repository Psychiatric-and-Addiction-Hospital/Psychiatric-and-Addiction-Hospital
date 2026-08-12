using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Shift
{
    public interface IGetShiftById
    {
        Task<BaseResponse<ShiftResponse>> GetByIdAsync(
      Guid id,
      CancellationToken ct);
    }
}
