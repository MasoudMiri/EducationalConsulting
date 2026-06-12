using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.DTOs
{
    public class ArticleUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string ExistingImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}