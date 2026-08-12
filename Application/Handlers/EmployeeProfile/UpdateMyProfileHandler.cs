using Application.Commands.EmployeeProfile;
using Application.Common.Interfaces.EmployeeProfile;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using Domain.Entites.ServicesModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.EmployeeProfile
{
    public class UpdateMyProfileHandler : IRequestHandler<UpdateMyProfileCommand, BaseResponse<EmployeeResponse>>
    {
        private readonly IUpdateMyProfile _service;
        public UpdateMyProfileHandler(IUpdateMyProfile service)
        {
            _service = service;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(UpdateMyProfileCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.request, cancellationToken);
        }
    }
}
