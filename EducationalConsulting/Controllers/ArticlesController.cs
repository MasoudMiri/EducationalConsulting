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
        public async Task<IActionResult> ListByCategory(string categoryName, int page = 1)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return NotFound();
            }

            ViewBag.CategoryName = categoryName;
            ViewBag.CurrentPage = page;

            var result = await _articleService.GetPagedArticlesByCategoryNameAsync(categoryName, page, 10);

            if (!result.Items.Any() && page > 1)
            {
                // اگه صفحه خالی بود و صفحه اول نبود، برو به صفحه اول
                return RedirectToAction("ListByCategory", new { categoryName, page = 1 });
            }

            return View(result);
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