using Application.Common.Interfaces.Admin;
using Application.Queries.Admin;
using Domain.Entites;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Admin.DoctorManagement
{
    public class GetPendingDoctorsHandler : IRequestHandler<GetPendingDoctorsQuery, List<DoctorApplication>>
    {
        private readonly IAdminDoctorManagement _AdminDoctorManagement;
        public GetPendingDoctorsHandler(IAdminDoctorManagement AdminDoctorManagement)
        {
            _AdminDoctorManagement = AdminDoctorManagement;
        }
        public async Task<List<DoctorApplication>> Handle(GetPendingDoctorsQuery request, CancellationToken ct)
        {
         return await _AdminDoctorManagement.GetPendingDoctorsAsync(ct);
        }
    }
}
