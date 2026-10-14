using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Attendance
{
    public class CheckOutHandler : IRequestHandler<CheckOutCommand, BaseResponse<AttendanceResponse>>
    {
        private readonly ICheckOutAttendance _service;
        private readonly ICurrentUser _currentUser;
        public CheckOutHandler(ICheckOutAttendance service, ICurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<AttendanceResponse>> Handle(CheckOutCommand request, CancellationToken ct)
        {
            return await _service.CheckOutAsync(_currentUser.UserId!, request.Request.Token, ct);
        }
    }
}
