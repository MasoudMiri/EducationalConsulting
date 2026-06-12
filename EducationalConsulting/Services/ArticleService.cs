using EducationalConsulting.Data;
using EducationalConsulting.DTOs;
using EducationalConsulting.Models;

namespace EducationalConsulting.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleService(
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ArticleListDto>> GetArticlesByCategoryNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            if (category == null)
                return new List<ArticleListDto>();

            var articles = await _articleRepository.GetActiveArticlesByCategoryIdAsync(category.Id);

            return articles.Select(a => new ArticleListDto
            {
                Id = a.Id,
                Title = a.Title,
                Summary = a.Summary,
                ImageUrl = a.ImageUrl,
                CreateDate = a.CreateDate,
                ViewCount = a.ViewCount,
                CategoryName = category.Name
            });
        }

        public async Task<ArticleDetailDto> GetArticleByIdAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
                return null;

            return new ArticleDetailDto
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                CreateDate = article.CreateDate,
                LastModifiedDate = article.LastModifiedDate,
                ViewCount = article.ViewCount,
                CategoryName = article.Category?.Name,
                CategoryId = article.CategoryId,
                IsActive = article.IsActive
            };
        }

        public async Task<bool> CreateArticleAsync(ArticleCreateDto articleCreateDto)
        {
            try
            {
                string imagePath = null;

                if (articleCreateDto.ImageFile != null)
                {
                    imagePath = await SaveImageAsync(articleCreateDto.ImageFile);
                }

                var article = new Article
                {
                    Title = articleCreateDto.Title,
                    Summary = articleCreateDto.Summary,
                    Content = articleCreateDto.Content,
                    CategoryId = articleCreateDto.CategoryId,
                    IsActive = articleCreateDto.IsActive,
                    ImageUrl = imagePath,
                    CreateDate = DateTime.Now,
                    ViewCount = 0
                };

                await _articleRepository.AddAsync(article);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(articleUpdateDto.Id);
                if (article == null)
                    return false;

                if (articleUpdateDto.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(article.ImageUrl))
                    {
                        DeleteOldImage(article.ImageUrl);
                    }
                    article.ImageUrl = await SaveImageAsync(articleUpdateDto.ImageFile);
                }

                article.Title = articleUpdateDto.Title;
                article.Summary = articleUpdateDto.Summary;
                article.Content = articleUpdateDto.Content;
                article.CategoryId = articleUpdateDto.CategoryId;
                article.IsActive = articleUpdateDto.IsActive;
                article.LastModifiedDate = DateTime.Now;

                await _articleRepository.UpdateAsync(article);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                if (article != null && !string.IsNullOrEmpty(article.ImageUrl))
                {
                    DeleteOldImage(article.ImageUrl);
                }
                await _articleRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task IncrementViewCountAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article != null)
            {
                article.ViewCount++;
                await _articleRepository.UpdateAsync(article);
            }
        }

        public async Task<IEnumerable<AdminArticlesViewModel>> GetAdminArticlesAsync()
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

        public async Task<IEnumerable<ArticleListDto>> GetLatestArticlesAsync(int count)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var allArticles = new List<ArticleListDto>();

            foreach (var category in categories)
            {
                var articles = await _articleRepository.GetActiveArticlesByCategoryIdAsync(category.Id);
                allArticles.AddRange(articles.Select(a => new ArticleListDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Summary = a.Summary,
                    ImageUrl = a.ImageUrl,
                    CreateDate = a.CreateDate,
                    ViewCount = a.ViewCount,
                    CategoryName = category.Name
                }));
            }

            return allArticles
                .OrderByDescending(a => a.CreateDate)
                .Take(count)
                .ToList();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "articles");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

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
            {
                File.Delete(fullPath);
            }
        }
    }
}