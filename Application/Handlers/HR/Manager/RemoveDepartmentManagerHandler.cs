using Application.Commands.HR.Manager;
using Application.Common.Interfaces.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Manager;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Manager
{
    public class RemoveDepartmentManagerHandler : IRequestHandler<RemoveDepartmentManagerCommand, BaseResponse<DepartmentManagerResponse>>
    {
        private readonly IRemoveDepartmentManager _service;
        public RemoveDepartmentManagerHandler(IRemoveDepartmentManager service)
        {
            _service = service;
        }

        public async Task<BaseResponse<DepartmentManagerResponse>> Handle(RemoveDepartmentManagerCommand request, CancellationToken cancellationToken)
        {
            return await _service.RemoveAsync(request.DepartmentId, cancellationToken);
        }
    }
}
