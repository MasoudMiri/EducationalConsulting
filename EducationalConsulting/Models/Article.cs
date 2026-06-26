using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalConsulting.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان مقاله الزامی است")]
        [Display(Name = "عنوان")]
        [MaxLength(200)]
        public string? Title { get; set; }

        [Display(Name = "خلاصه")]
        [MaxLength(500)]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "محتوا الزامی است")]
        [Display(Name = "محتوا")]
        public string? Content { get; set; }

        [Display(Name = "آدرس عکس")]
        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; } = 0;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "تاریخ آخرین ویرایش")]
        public DateTime? LastModifiedDate { get; set; }

        
        [Display(Name = "دسته‌بندی")]
        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است")]
        public int CategoryId { get; set; }

        
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}