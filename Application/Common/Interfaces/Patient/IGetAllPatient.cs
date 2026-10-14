using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IGetAllPatient
    {
        Task<BaseResponse<PagedResponse<PatientProfileResponse>>> GetAllAsync(PatientListRequest request, CancellationToken ct);
    }
}
