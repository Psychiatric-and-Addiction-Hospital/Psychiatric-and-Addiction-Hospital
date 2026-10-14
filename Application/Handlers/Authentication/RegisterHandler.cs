using Application.Commands.Authentication;
using Application.Common.Constants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, BaseResponse<RegisterResponse>>
    {

        private readonly IRegister _service;
        public RegisterHandler(IRegister service)
        {
            _service = service;
        }
        public async Task<BaseResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _service.RegisterAsync(request.request, cancellationToken);

        }

    }
}

