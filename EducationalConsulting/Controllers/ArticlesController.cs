using Microsoft.AspNetCore.Mvc;
using EducationalConsulting.Services;

namespace EducationalConsulting.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // نمایش لیست مقالات بر اساس دسته‌بندی
        public async Task<IActionResult> ListByCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return NotFound();
            }

            var articles = await _articleService.GetArticlesByCategoryNameAsync(categoryName);

            // ذخیره نام دسته برای نمایش در View
            ViewBag.CategoryName = categoryName;

            return View(articles);
        }

        // نمایش جزئیات یک مقاله
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            // افزایش تعداد بازدید
            await _articleService.IncrementViewCountAsync(id);

            return View(article);
        }
    }
}