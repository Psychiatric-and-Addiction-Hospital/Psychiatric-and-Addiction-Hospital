using Application.Common.Interfaces.Admin;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Admin;
using Domain.Entites.DoctorsModule;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class GetPendingDoctorsHandler : IRequestHandler<GetPendingDoctorsQuery, BaseResponse<List<PendingDoctorApplicationResponse>>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public GetPendingDoctorsHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<BaseResponse<List<PendingDoctorApplicationResponse>>> Handle(GetPendingDoctorsQuery request, CancellationToken ct)
        {
            return await _AdminDoctorManagement.GetPendingDoctorsAsync(ct);
        }
    }
}
