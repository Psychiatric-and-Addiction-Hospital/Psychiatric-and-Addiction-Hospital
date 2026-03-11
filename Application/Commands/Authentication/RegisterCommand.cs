using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Commands.Authentication
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        Gender gender,
        string Addres,
        string Password,
        string ConfirmPassword
    ) : IRequest<BaseResponse<RegisterResponse>>;

}
