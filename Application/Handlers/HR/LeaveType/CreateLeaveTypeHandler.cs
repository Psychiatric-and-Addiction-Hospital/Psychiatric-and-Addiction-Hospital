using Application.Commands.HR.LeaveType;
using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveType
{
    public class CreateLeaveTypeHandler : IRequestHandler<CreateLeaveTypeCommand, BaseResponse<LeaveTypeResponse>>
    {
        private readonly ICreateLeaveType _service;

        public CreateLeaveTypeHandler(ICreateLeaveType service)
        {
            _service = service;
        }

        public async Task<BaseResponse<LeaveTypeResponse>> Handle(CreateLeaveTypeCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request.LeaveType, ct);
        }
    }
}
