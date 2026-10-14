using Application.Common.Interfaces.UpLoad;
namespace Psychiatric_and_Addiction_Hospital.Services.FileStorage
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;

        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        private static readonly string[] DocumentExtensions = { ".pdf", ".doc", ".docx" };
        public LocalFileStorage(IWebHostEnvironment env)
        {
            _env = env;
        }
        public bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            return ImageExtensions.Contains(extension);
        }
        public bool IsValidDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            return DocumentExtensions.Contains(extension);
        }

        public async Task<string?> SaveFileAsync(IFormFile file, string folderName, CancellationToken ct)
        {
            if (file == null || file.Length == 0) return null;

            var folder = Path.Combine(_env.WebRootPath, folderName);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(folder, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream, ct);


            return $"{folderName}/{fileName}";
        }

        public Task DeleteFileAsync(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return Task.CompletedTask;

            var fullPath = Path.Combine(
                _env.WebRootPath,
                filePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}