using EducationalConsulting.DTOs;
using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.Services
{
    public interface IAdminService
    {
        Task<bool> IsLoggedInAsync(ISession session);
        Task<bool> LoginAsync(LoginDto model, ISession session);
        void Logout(ISession session);
        Task<IEnumerable<AdminArticlesViewModel>> GetAllArticlesForAdminAsync();
        Task<ServiceResult> CreateArticleAsync(ArticleCreateDto model);
        Task<ServiceResult<ArticleUpdateDto>> GetArticleForEditAsync(int id);
        Task<ServiceResult> UpdateArticleAsync(ArticleUpdateDto model);
        Task<ServiceResult> DeleteArticleAsync(int id);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}