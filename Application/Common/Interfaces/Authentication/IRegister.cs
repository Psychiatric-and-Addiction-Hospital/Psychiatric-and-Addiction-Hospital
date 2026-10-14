using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IRegister
    {
        Task<BaseResponse<RegisterResponse>> RegisterAsync(CreatePatientProfileRequest request, CancellationToken ct);
    }
}
