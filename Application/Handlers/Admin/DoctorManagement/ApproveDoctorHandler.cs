using Application.Commands.Admin;
using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Doctores;
using Application.Common.Responses;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class ApproveDoctorHandler : IRequestHandler<ApproveDoctorCommand, BaseResponse<string>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public ApproveDoctorHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<BaseResponse<string>> Handle(ApproveDoctorCommand request, CancellationToken cancellationToken)
        {
            return await _AdminDoctorManagement.ApplyForDoctorAsync(request.ApplicationId,request.DepartmentId, cancellationToken);
        }
    }
}
