using Application.Commands.HR.Department;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Depertment
{

    public class DeleteDepartmentHandler
        :IRequestHandler<DeleteDepartmentCommand, BaseResponse<DepartmentResponse>>


    {
        private readonly IDeleteDepartment _deleteDepartment;

        public DeleteDepartmentHandler(IDeleteDepartment deleteDepartment)
        {
            _deleteDepartment = deleteDepartment;
        }

        public async Task<BaseResponse<DepartmentResponse>> Handle(DeleteDepartmentCommand request, CancellationToken ct)
        {
            return await _deleteDepartment.DeleteDepartmentAsync(request.Id, ct);
        }
    
    }
}
