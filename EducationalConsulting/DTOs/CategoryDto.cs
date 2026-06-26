using System.ComponentModel.DataAnnotations;

namespace EducationalConsulting.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام دسته الزامی است")]
        [MaxLength(100, ErrorMessage = "نام دسته نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}