using Application.Commands.Admin;
using Application.Common.Interfaces.Admin;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class RejectDoctorHandler : IRequestHandler<RejectDoctorCommand, Result<string>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public RejectDoctorHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<Result<string>> Handle(RejectDoctorCommand request, CancellationToken ct)
        {
            return await _AdminDoctorManagement.RejectDoctorAsync(request.ApplicationId, request.Reason, ct);
        }
    }
}
