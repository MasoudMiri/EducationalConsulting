using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EducationalConsulting.DTOs
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان مقاله الزامی است")]
        [MaxLength(200, ErrorMessage = "عنوان نمی‌تواند بیشتر از ۲۰۰ کاراکتر باشد")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "خلاصه نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "محتوا الزامی است")]
        public string Content { get; set; }
        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است")]
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string ExistingImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}