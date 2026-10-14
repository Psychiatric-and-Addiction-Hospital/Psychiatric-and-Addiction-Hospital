using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using MediatR;

namespace Application.Commands.HR.Shift
{
    public record UpdateShiftCommand(UpdateShiftRequest request) : IRequest<BaseResponse<ShiftResponse>>;

}
