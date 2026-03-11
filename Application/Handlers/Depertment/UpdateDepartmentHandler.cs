using Application.Commands.Department;
using Application.Common.Interfaces.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Depertment
{
    public class UpdateDepartmentHandler
    : IRequestHandler<UpdateDepartmentCommand, BaseResponse<DepertmentResponse>>
    {
        private readonly IUpdateDepartment _updateService;

        public UpdateDepartmentHandler(IUpdateDepartment updateService)
        {
            _updateService = updateService;
        }

        public async Task<BaseResponse<DepertmentResponse>> Handle(UpdateDepartmentCommand request, CancellationToken ct)
        {
            return await _updateService.UpdateAsync(request.Id, request.Name, request.Description, ct);
        }
    }
}