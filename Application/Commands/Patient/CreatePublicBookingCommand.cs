using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Commands.Patient
{
    public record CreatePublicBookingCommand(CreatePublicBookingRequest request) : IRequest<BaseResponse<PublicBookingResponse>>;
}
