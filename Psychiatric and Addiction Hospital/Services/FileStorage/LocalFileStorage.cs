using Application.Common.Interfaces.UpLoad;
namespace Psychiatric_and_Addiction_Hospital.Services.FileStorage
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        public LocalFileStorage(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string?> SaveDoctorImageAsync(IFormFile file, CancellationToken ct)
        {
            if (file == null) return null;

            var folder = Path.Combine(_env.WebRootPath, "doctor-images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream, ct);
            }

            return $"doctor-images/{fileName}";
        }
    }
}