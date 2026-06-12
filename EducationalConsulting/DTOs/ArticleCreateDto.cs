using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.DTOs
{
    public class ArticleCreateDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public IFormFile ImageFile { get; set; }  // برای آپلود عکس
    }
}