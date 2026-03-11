using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.ManagementDoctor
{
    public interface IGetAllDoctors
    {
        Task<BaseResponse<List<DoctorProfileResponse>>> GetAllDoctorsAsync(CancellationToken ct);
    }
}
