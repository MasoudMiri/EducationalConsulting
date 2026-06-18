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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleService.GetLatestArticlesAsync(5);
            return View(articles);
        }
    }
}