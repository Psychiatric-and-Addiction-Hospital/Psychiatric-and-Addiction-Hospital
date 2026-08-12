using Application.Commands.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Shift
{
    public interface ICreateShift
    {
        Task<BaseResponse<ShiftResponse>> CreateAsync(
           CreateShiftCommand command, CancellationToken ct);
    }
}
