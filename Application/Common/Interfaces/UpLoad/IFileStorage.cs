using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.UpLoad
{
    public interface IFileStorage
    {
        Task<string?> SaveDoctorImageAsync(IFormFile file, CancellationToken ct);
    }
}
