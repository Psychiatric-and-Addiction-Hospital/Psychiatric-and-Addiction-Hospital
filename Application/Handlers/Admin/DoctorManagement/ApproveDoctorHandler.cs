using Application.Commands.Admin;
using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Doctores;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class ApproveDoctorHandler : IRequestHandler<ApproveDoctorCommand, Result<string>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public ApproveDoctorHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<Result<string>> Handle(ApproveDoctorCommand request, CancellationToken cancellationToken)
        {
            return await _AdminDoctorManagement.ApplyForDoctorAsync(request.ApplicationId, cancellationToken);
        }
    }
}
