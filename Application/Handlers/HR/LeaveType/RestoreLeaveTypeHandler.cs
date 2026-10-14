using Application.Commands.HR.LeaveType;
using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveType
{
    public class RestoreLeaveTypeHandler : IRequestHandler<RestoreLeaveTypeCommand, BaseResponse<bool>>
    {
        private readonly IRestoreLeaveType _service;
        public RestoreLeaveTypeHandler(IRestoreLeaveType service)
        {
            _service = service;
        }
        public Task<BaseResponse<bool>> Handle(RestoreLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            return _service.RestoreAsync(request.LeaveTypeId, cancellationToken);
        }
    }
}
