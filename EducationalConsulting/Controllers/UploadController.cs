using Microsoft.AspNetCore.Mvc;
using EducationalConsulting.Services;

namespace EducationalConsulting.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileService _fileService;

        public UploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile upload)
        {
            // 1. چک کردن وجود فایل
            if (upload == null || upload.Length == 0)
                return Json(new { uploaded = false, error = new { message = "لطفاً یک فایل انتخاب کنید." } });

            // 2. چک کردن حجم (حداکثر ۱۰۰ مگابایت)
            if (upload.Length > 100 * 1024 * 1024)
                return Json(new { uploaded = false, error = new { message = "حجم فایل بیش از ۱۰۰ مگابایت است." } });

            // 3. تشخیص نوع فایل
            var extension = Path.GetExtension(upload.FileName).ToLower();
            var isImage = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" }.Contains(extension);
            var isVideo = new[] { ".mp4", ".webm", ".ogg", ".mov", ".avi" }.Contains(extension);

            if (!isImage && !isVideo)
                return Json(new { uploaded = false, error = new { message = "فرمت فایل پشتیبانی نمی‌شود." } });

            try
            {
                string filePath = isImage
                    ? await _fileService.SaveImageAsync(upload, "articles")
                    : await _fileService.SaveVideoAsync(upload, "articles");

                return Json(new { uploaded = true, url = filePath });
            }
            catch (Exception ex)
            {
                return Json(new { uploaded = false, error = new { message = $"خطا در آپلود: {ex.Message}" } });
            }
        }
    }
}