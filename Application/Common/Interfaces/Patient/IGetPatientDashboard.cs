using Application.Common.Responses;
using Application.DTOS.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Patient
{
    public interface IGetPatientDashboard
    {
        Task<BaseResponse<PatientDashboardResponse>> GetDashboardAsync(string patientId, CancellationToken ct);
    }
}
