using Application.Commands.HR.Department;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Depertment
{

    public class DeleteDepartmentHandler
        :IRequestHandler<DeleteDepartmentCommand, BaseResponse<DepertmentResponse>>


    {
        private readonly IDeleteDepartment _deleteDepartment;

        public DeleteDepartmentHandler(IDeleteDepartment deleteDepartment)
        {
            _deleteDepartment = deleteDepartment;
        }

        public async Task<BaseResponse<DepertmentResponse>> Handle(DeleteDepartmentCommand request, CancellationToken ct)
        {
            return await _deleteDepartment.DeleteDepartment(request.Id, ct);
        }
    
    }
}
