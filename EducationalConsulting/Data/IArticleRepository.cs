using EducationalConsulting.DTOs;
using EducationalConsulting.Models;

namespace EducationalConsulting.Data
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Article>> GetActiveArticlesByCategoryIdAsync(int categoryId);
        Task<Article?> GetByIdAsync(int id);
        Task AddAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(int id);
        Task<bool> ArticleExistsAsync(int id);
        Task<IEnumerable<ArticleListDto>> GetLatestArticlesAsync(int count);

    }
}