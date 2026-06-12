using EducationalConsulting.Data;
using EducationalConsulting.DTOs;
using EducationalConsulting.Models;
using Microsoft.AspNetCore.Http;

namespace EducationalConsulting.Services
{
    public class AdminService : IAdminService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string SessionKey = "AdminLoggedIn";

        public AdminService(
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> IsLoggedInAsync(ISession session)
        {
            return await Task.FromResult(session.GetString(SessionKey) == "true");
        }

        public async Task<bool> LoginAsync(LoginDto model, ISession session)
        {
            // TODO: بعداً از دیتابیس بررسی کن
            if (model.Username == "admin" && model.Password == "123456")
            {
                session.SetString(SessionKey, "true");
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public void Logout(ISession session)
        {
            session.Remove(SessionKey);
        }

        public async Task<IEnumerable<AdminArticlesViewModel>> GetAllArticlesForAdminAsync()
        {
            var allArticles = new List<AdminArticlesViewModel>();
            var categories = await _categoryRepository.GetAllAsync();

            foreach (var category in categories)
            {
                var articles = await _articleRepository.GetByCategoryIdAsync(category.Id);
                allArticles.AddRange(articles.Select(a => new AdminArticlesViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    CategoryName = category.Name,
                    CreateDate = a.CreateDate,
                    ViewCount = a.ViewCount,
                    IsActive = a.IsActive
                }));
            }

            return allArticles.OrderByDescending(a => a.CreateDate);
        }

        public async Task<ServiceResult> CreateArticleAsync(ArticleCreateDto model)
        {
            try
            {
                string imagePath = null;
                if (model.ImageFile != null)
                {
                    imagePath = await SaveImageAsync(model.ImageFile);
                }

                var article = new Article
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    IsActive = model.IsActive,
                    ImageUrl = imagePath,
                    CreateDate = DateTime.Now,
                    ViewCount = 0
                };

                await _articleRepository.AddAsync(article);
                return ServiceResult.Ok("مقاله با موفقیت اضافه شد");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"خطا در افزودن مقاله: {ex.Message}");
            }
        }

        public async Task<ServiceResult<ArticleUpdateDto>> GetArticleForEditAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
                return ServiceResult.Fail<ArticleUpdateDto>("مقاله یافت نشد");

            var model = new ArticleUpdateDto
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Content = article.Content,
                CategoryId = article.CategoryId,
                IsActive = article.IsActive,
                ExistingImageUrl = article.ImageUrl
            };

            return ServiceResult.Ok(model);
        }

        public async Task<ServiceResult> UpdateArticleAsync(ArticleUpdateDto model)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(model.Id);
                if (article == null)
                    return ServiceResult.Fail("مقاله یافت نشد");

                if (model.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(article.ImageUrl))
                        DeleteOldImage(article.ImageUrl);
                    article.ImageUrl = await SaveImageAsync(model.ImageFile);
                }

                article.Title = model.Title;
                article.Summary = model.Summary;
                article.Content = model.Content;
                article.CategoryId = model.CategoryId;
                article.IsActive = model.IsActive;
                article.LastModifiedDate = DateTime.Now;

                await _articleRepository.UpdateAsync(article);
                return ServiceResult.Ok("مقاله با موفقیت ویرایش شد");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"خطا در ویرایش مقاله: {ex.Message}");
            }
        }

        public async Task<ServiceResult> DeleteArticleAsync(int id)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                if (article != null && !string.IsNullOrEmpty(article.ImageUrl))
                    DeleteOldImage(article.ImageUrl);

                await _articleRepository.DeleteAsync(id);
                return ServiceResult.Ok("مقاله با موفقیت حذف شد");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"خطا در حذف مقاله: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive
            });
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "articles");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/articles/" + uniqueFileName;
        }

        private void DeleteOldImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}