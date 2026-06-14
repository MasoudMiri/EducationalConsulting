using EducationalConsulting.DTOs;

namespace EducationalConsulting.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleListDto>> GetArticlesByCategoryNameAsync(string categoryName);
        Task<ArticleDetailDto> GetArticleByIdAsync(int id);
        Task<bool> CreateArticleAsync(ArticleCreateDto articleCreateDto);
        Task<bool> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
        Task<bool> DeleteArticleAsync(int id);
        Task IncrementViewCountAsync(int id);
        Task<IEnumerable<AdminArticlesViewModel>> GetAdminArticlesAsync();
        Task<IEnumerable<ArticleListDto>> GetLatestArticlesAsync(int count);  // ← این خط جدید
        Task<PagedResult<ArticleListDto>> GetPagedArticlesByCategoryNameAsync(string categoryName, int page, int pageSize = 10);

    }
}