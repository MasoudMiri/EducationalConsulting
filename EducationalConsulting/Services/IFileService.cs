using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string subFolder = "articles");
        Task<string> SaveVideoAsync(IFormFile videoFile, string subFolder = "articles");

        void DeleteFile(string filePath);
        void DeleteImage(string imagePath);  

        bool FileExists(string filePath);
        bool IsValidImage(IFormFile file);
        bool IsValidVideo(IFormFile file);
    }
}