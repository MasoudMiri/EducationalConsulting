using EducationalConsulting.DTOs;

namespace EducationalConsulting.Services
{
    public interface IAdminService
    {
        // ============================================================
        //  مدیریت مقالات
        // ============================================================

        /// <summary>
        /// دریافت تمام مقالات برای نمایش در پنل ادمین
        /// </summary>
        Task<IEnumerable<AdminArticlesViewModel>> GetAllArticlesForAdminAsync();

        /// <summary>
        /// ایجاد مقاله جدید
        /// </summary>
        Task<ServiceResult> CreateArticleAsync(ArticleCreateDto model);

        /// <summary>
        /// دریافت اطلاعات مقاله برای ویرایش
        /// </summary>
        Task<ServiceResult<ArticleUpdateDto>> GetArticleForEditAsync(int id);

        /// <summary>
        /// ویرایش مقاله
        /// </summary>
        Task<ServiceResult> UpdateArticleAsync(ArticleUpdateDto model);

        /// <summary>
        /// حذف مقاله
        /// </summary>
        Task<ServiceResult> DeleteArticleAsync(int id);

        // ============================================================
        //  مدیریت دسته‌بندی‌ها
        // ============================================================

        /// <summary>
        /// دریافت لیست تمام دسته‌بندی‌ها
        /// </summary>
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}