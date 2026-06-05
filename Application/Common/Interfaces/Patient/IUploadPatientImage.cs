using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IUploadPatientImage
    {
        Task<BaseResponse<PatientProfileResponse>> UploadImageAsync(string userId, string imageUrl, CancellationToken ct);
    }
}
