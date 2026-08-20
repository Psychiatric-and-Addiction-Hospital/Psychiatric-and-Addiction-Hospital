using Application.Commands.Patient;
using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IUpdatePatientProfile
    {
        Task<BaseResponse<PatientProfileResponse>> UpdateAsync(UpdatePatientProfileRequest request, CancellationToken ct);
    }
}
