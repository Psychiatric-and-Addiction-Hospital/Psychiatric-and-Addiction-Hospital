using Application.Commands.HR.LeaveType;
using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveType
{
    public class UpdateLeaveTypeHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseResponse<LeaveTypeResponse>>
    {
        private readonly IUpdateLeaveType _service;

        public UpdateLeaveTypeHandler(IUpdateLeaveType service)
        {
            _service = service;
        }

        public async Task<BaseResponse<LeaveTypeResponse>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.request, cancellationToken);
        }
    }
}
