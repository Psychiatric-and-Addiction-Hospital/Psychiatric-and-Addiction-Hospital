using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.UpLoad
{
    public interface IFileStorage
    {
        Task<string?> SaveFileAsync(IFormFile file, string folderName, CancellationToken ct);
        bool IsValidImage(IFormFile file);

        bool IsValidDocument(IFormFile file);

        Task DeleteFileAsync(string? filePath);
    }
}
