using Application.Commands.HR.Shift;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Shift
{
    public class DeleteShiftHandler : IRequestHandler<DeleteShiftCommand, BaseResponse<ShiftResponse>>
    {
        private readonly IDeleteShift _service;

        public DeleteShiftHandler(IDeleteShift service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ShiftResponse>> Handle(
            DeleteShiftCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request, cancellationToken);
        }
    }

}
