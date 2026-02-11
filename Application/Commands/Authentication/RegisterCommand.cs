using FluentResults;
using MediatR;

namespace Application.Commands.Authentication
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Addres,
        string Password,
        string ConfirmPassword
    ) : IRequest<Result<string>>;

}
