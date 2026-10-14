using Application.Commands.HR.LeaveRequest;
using Application.Common.Interfaces.HR.LeaveRequest;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.LeaveRequest
{
    public class CreateLeaveRequestHandler : IRequestHandler<CreateLeaveRequestCommand, BaseResponse<LeaveRequestResponse>>
    {
        private readonly ICreateLeaveRequest _service;
        public CreateLeaveRequestHandler(ICreateLeaveRequest service)
        {
            _service = service;
        }
        public async Task<BaseResponse<LeaveRequestResponse>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(request.request, cancellationToken);
        }
    }
}
