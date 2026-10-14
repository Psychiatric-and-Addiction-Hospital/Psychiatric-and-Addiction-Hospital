using Application.Commands.HR.Manager;
using Application.Common.Interfaces.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Manager;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Manager
{
    public class ChangeDepartmentManagerHandler : IRequestHandler<ChangeDepartmentManagerCommand, BaseResponse<DepartmentManagerResponse>>
    {
        private readonly IChangeDepartmentManager _service;
        public ChangeDepartmentManagerHandler(IChangeDepartmentManager service)
        {
            _service = service;
        }
        public async Task<BaseResponse<DepartmentManagerResponse>> Handle(ChangeDepartmentManagerCommand request, CancellationToken cancellationToken)
        {
            return await _service.ChangeAsync(request.request, cancellationToken);
        }
    }
}
