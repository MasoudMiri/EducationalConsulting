using EducationalConsulting.DTOs;
using EducationalConsulting.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalConsulting.Data
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles
                .Include(a => a.Category)
                .OrderByDescending(a => a.CreateDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == categoryId)
                .OrderByDescending(a => a.CreateDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetActiveArticlesByCategoryIdAsync(int categoryId)
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == categoryId && a.IsActive)
                .OrderByDescending(a => a.CreateDate)
                .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Article article)
        {
            article.CreateDate = DateTime.Now;
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Article article)
        {
            article.LastModifiedDate = DateTime.Now;
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await GetByIdAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ArticleExistsAsync(int id)
        {
            return await _context.Articles.AnyAsync(e => e.Id == id);
        }

        
        public async Task<IEnumerable<ArticleListDto>> GetLatestArticlesAsync(int count)
        {
            var articles = await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.CreateDate)
                .Take(count)
                .Select(a => new ArticleListDto
                {
                    Id = a.Id,
                    Title = a.Title?? string.Empty,
                    Summary = a.Summary ?? string.Empty,
                    ImageUrl = a.ImageUrl ?? string.Empty,
                    CreateDate = a.CreateDate,
                    ViewCount = a.ViewCount,
                    CategoryName = a.Category != null ? a.Category.Name : ""
                })
                .ToListAsync();

            return articles;
        }
    }
}