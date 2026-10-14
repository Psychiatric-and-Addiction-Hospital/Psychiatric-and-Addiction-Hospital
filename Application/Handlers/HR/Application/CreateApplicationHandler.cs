using Application.Commands.HR.Application;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Application
{
    public class CreateApplicationHandler : IRequestHandler<CreateApplicationCommand, BaseResponse<ApplicationResponse>>
    {
        private readonly ICreateApplication _service;
        public CreateApplicationHandler(ICreateApplication service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationResponse>> Handle(CreateApplicationCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request, ct);
        }
    }
}
