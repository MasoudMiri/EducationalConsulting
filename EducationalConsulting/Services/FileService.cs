using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // ===== ذخیره عکس =====
        public async Task<string> SaveImageAsync(IFormFile imageFile, string subFolder = "articles")
        {
            if (!IsValidImage(imageFile))
                throw new ArgumentException("فرمت عکس مجاز نیست.");

            return await SaveFileAsync(imageFile, subFolder, "images");
        }

        // ===== ذخیره فیلم =====
        public async Task<string> SaveVideoAsync(IFormFile videoFile, string subFolder = "articles")
        {
            if (!IsValidVideo(videoFile))
                throw new ArgumentException("فرمت فیلم مجاز نیست.");

            return await SaveFileAsync(videoFile, subFolder, "videos");
        }

        // ===== متد اصلی ذخیره فایل =====
        private async Task<string> SaveFileAsync(IFormFile file, string subFolder, string fileType)
        {
            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");

            string uploadsFolder = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "uploads",
                subFolder,
                fileType,
                year,
                month
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/{subFolder}/{fileType}/{year}/{month}/{uniqueFileName}";
        }

        // ===== حذف فایل =====
        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        // ===== حذف عکس (همون DeleteFile) =====
        public void DeleteImage(string imagePath)
        {
            DeleteFile(imagePath);  // ← جدید
        }

        // ===== بررسی وجود فایل =====
        public bool FileExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));
            return File.Exists(fullPath);
        }

        // ===== اعتبارسنجی عکس =====
        public bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };

            if (!allowedExtensions.Contains(extension))
                return false;

            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp" };
            return allowedMimeTypes.Contains(file.ContentType);
        }

        // ===== اعتبارسنجی فیلم =====
        public bool IsValidVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new[] { ".mp4", ".webm", ".ogg", ".mov", ".avi" };

            if (!allowedExtensions.Contains(extension))
                return false;

            var allowedMimeTypes = new[] { "video/mp4", "video/webm", "video/ogg", "video/quicktime", "video/x-msvideo" };
            return allowedMimeTypes.Contains(file.ContentType);
        }
    }
}