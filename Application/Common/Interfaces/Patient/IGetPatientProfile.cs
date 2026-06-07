using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IGetPatientProfile
    {
        Task<BaseResponse<PatientProfileResponse>> GetProfileAsync(string userId, CancellationToken ct);
    }
}
