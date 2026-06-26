using System.ComponentModel.DataAnnotations;

namespace EducationalConsulting.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام دسته الزامی است")]
        [Display(Name = "نام دسته")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        
        public virtual ICollection<Article>? Articles { get; set; }
    }
}