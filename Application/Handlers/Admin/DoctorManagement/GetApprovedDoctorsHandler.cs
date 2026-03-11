using Application.Common.Interfaces.Admin;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.Queries.Admin;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class GetApprovedDoctorsHandler : IRequestHandler<GetApprovedDoctorsQuery, BaseResponse<List<ApprovedDoctorApplicationRespons>>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public GetApprovedDoctorsHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<BaseResponse<List<ApprovedDoctorApplicationRespons>>> Handle(GetApprovedDoctorsQuery request, CancellationToken ct)
        {
            return await _AdminDoctorManagement.GetApprovedDoctorsAsync(ct);
        }
    }
}
