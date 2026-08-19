using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using MediatR;

namespace Application.Commands.Authentication
{
    public record RegisterCommand(CreatePatientProfileRequest request) : IRequest<BaseResponse<RegisterResponse>>;

}
