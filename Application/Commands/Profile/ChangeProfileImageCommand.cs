using Application.Common.Responses;
using Application.DTOS.Request.Profile;
using MediatR;

namespace Application.Commands.Profile
{
    public record ChangeProfileImageCommand(ChangeProfileImageRequest request) : IRequest<BaseResponse<string>>;
}
