using Application.Commands.HR.LeaveType;
using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveType
{
    public class DeleteLeaveTypeHandler : IRequestHandler<DeleteLeaveTypeCommand, BaseResponse<bool>>
    {
        private readonly IDeleteLeaveType _service;
        public DeleteLeaveTypeHandler(IDeleteLeaveType service)
        {
            _service = service;
        }
        public async Task<BaseResponse<bool>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.request, cancellationToken);
        }
    }
}
