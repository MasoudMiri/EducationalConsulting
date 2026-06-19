using EducationalConsulting.Data;
using EducationalConsulting.DTOs;
using EducationalConsulting.Models;

namespace EducationalConsulting.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;

        public ArticleService(
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            IFileService fileService)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _fileService = fileService;
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
                    imagePath = await _fileService.SaveImageAsync(articleCreateDto.ImageFile, "articles");
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
                        _fileService.DeleteImage(article.ImageUrl);
                    }
                    article.ImageUrl = await _fileService.SaveImageAsync(articleUpdateDto.ImageFile, "articles");
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
                    _fileService.DeleteImage(article.ImageUrl);
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

        public async Task<PagedResult<ArticleListDto>> GetPagedArticlesByCategoryNameAsync(string categoryName, int page, int pageSize = 10)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            if (category == null)
            {
                return new PagedResult<ArticleListDto>
                {
                    Items = new List<ArticleListDto>(),
                    Pagination = new PaginationDto { CurrentPage = page, PageSize = pageSize, TotalCount = 0 }
                };
            }

            var allArticles = await _articleRepository.GetActiveArticlesByCategoryIdAsync(category.Id);
            var totalCount = allArticles.Count();

            var pagedArticles = allArticles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ArticleListDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Summary = a.Summary,
                    ImageUrl = a.ImageUrl,
                    CreateDate = a.CreateDate,
                    ViewCount = a.ViewCount,
                    CategoryName = category.Name
                });

            return new PagedResult<ArticleListDto>
            {
                Items = pagedArticles,
                Pagination = new PaginationDto
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount
                }
            };
        }
    }
}