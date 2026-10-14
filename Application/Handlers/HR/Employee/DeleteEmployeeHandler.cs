using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IDeleteEmployee _service;
        public DeleteEmployeeHandler(IDeleteEmployee service)
        {
            _service = service;
        }
        public async Task<BaseResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.request, cancellationToken);
        }
    }
}
