using Microsoft.AspNetCore.Mvc;
using EducationalConsulting.Services;

namespace EducationalConsulting.Components
{
    public class ImportantArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public ImportantArticlesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 5)
        {
            var articles = await _articleService.GetLatestArticlesAsync(count);
            return View(articles);
        }
    }
}