using Application.DTOS.Responses;
using FluentResults;
using MediatR;


namespace Application.Commands.Authentication
{
    public record LoginCommand(
        string Email,
         string Password): IRequest<Result<AuthResult>>;


}
