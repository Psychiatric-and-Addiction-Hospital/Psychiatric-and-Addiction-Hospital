using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CVFilePathValidationService : ICVFilePathValidationService
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public bool IsValidCV(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            var extension = Path.GetExtension(filePath)?.ToLower();

            string[] allowedExtensions = { ".pdf", ".doc", ".docx" };
            if (!allowedExtensions.Contains(extension))
                return false;

            if (!File.Exists(filePath))
                return false;

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > MaxFileSize)
                return false;

            return true;
        }
    }
}
