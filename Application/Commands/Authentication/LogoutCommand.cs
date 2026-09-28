using Application.Common.Responses;
using MediatR;

namespace Application.Commands.Authentication
{
    public record LogoutCommand(string UserId) : IRequest<BaseResponse<Unit>>;
}
