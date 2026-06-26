using EducationalConsulting.DTOs;

namespace EducationalConsulting.Services
{
    public interface IAdminService
    {

        Task<IEnumerable<AdminArticlesViewModel>> GetAllArticlesForAdminAsync();


        Task<ServiceResult> CreateArticleAsync(ArticleCreateDto model);


        Task<ServiceResult<ArticleUpdateDto>> GetArticleForEditAsync(int id);

        Task<ServiceResult> UpdateArticleAsync(ArticleUpdateDto model);


        Task<ServiceResult> DeleteArticleAsync(int id);


        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}