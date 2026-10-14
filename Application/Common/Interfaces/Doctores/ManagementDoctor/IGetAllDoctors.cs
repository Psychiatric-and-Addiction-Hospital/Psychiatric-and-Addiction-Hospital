using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.ManagementDoctor
{
    public interface IGetAllDoctors
    {
        Task<BaseResponse<PagedResponse<DoctorProfileResponse>>> GetAllDoctorsAsync(DoctorListRequest request, CancellationToken ct);
    }
}
