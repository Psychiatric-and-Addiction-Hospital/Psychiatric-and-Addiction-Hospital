using Application.Commands.HR.Application;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Application
{
    public class DeleteApplicationHandler : IRequestHandler<DeleteApplicationCommand, BaseResponse<bool>>
    {
        private readonly IDeleteApplication _service;

        public DeleteApplicationHandler(IDeleteApplication service)
        {
            _service = service;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
