using Application.Common.Interfaces.Admin;
using Application.Queries.Admin;
using Domain.Entites;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class GetApprovedDoctorsHandler : IRequestHandler<GetApprovedDoctorsQuery, List<DoctorApplication>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public GetApprovedDoctorsHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<List<DoctorApplication>> Handle(GetApprovedDoctorsQuery request, CancellationToken ct)
        {
            return await _AdminDoctorManagement.GetApprovedDoctorsAsync(ct);
        }
    }
}
