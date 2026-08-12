using Application.Commands.HR.Manager;
using Application.Common.Interfaces.HR.Manager;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Manager;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Manager
{
    public class AssignDepartmentManagerHandler : IRequestHandler<AssignDepartmentManagerCommand, BaseResponse<DepartmentManagerResponse>>
    {
        private readonly IAssignDepartmentManager _service;
        public AssignDepartmentManagerHandler(IAssignDepartmentManager service)
        {
            _service = service;
        }
        public async Task<BaseResponse<DepartmentManagerResponse>> Handle(AssignDepartmentManagerCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignAsync(request.request, cancellationToken);
        }
    }
}
