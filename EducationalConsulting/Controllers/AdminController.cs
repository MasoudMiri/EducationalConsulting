using EducationalConsulting.Data;
using EducationalConsulting.DTOs;
using EducationalConsulting.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalConsulting.Controllers
{
    public class AdminController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthService _authService;

        public AdminController(
            IArticleService articleService,
            ICategoryRepository categoryRepository,
            IAuthService authService)
        {
            _articleService = articleService;
            _categoryRepository = categoryRepository;
            _authService = authService;
        }

        // صفحه ورود
        public IActionResult Login()
        {
            if (_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (_authService.ValidateAdminLogin(username, password))
            {
                _authService.SetAdminLogin(HttpContext.Session);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است";
            return View();
        }

        // خروج
        public IActionResult Logout()
        {
            _authService.Logout(HttpContext.Session);
            return RedirectToAction("Login");
        }

        // پنل اصلی
        public IActionResult Index()
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");
            return View();
        }

        // لیست مقالات
        public async Task<IActionResult> Articles()
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            var articles = await _articleService.GetAdminArticlesAsync();
            return View(articles);
        }

        // افزودن مقاله
        public async Task<IActionResult> CreateArticle()
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleCreateDto model)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepository.GetAllAsync();
                return View(model);
            }

            var result = await _articleService.CreateArticleAsync(model);
            if (result)
            {
                TempData["Success"] = "مقاله با موفقیت اضافه شد";
                return RedirectToAction("Articles");
            }

            TempData["Error"] = "خطا در افزودن مقاله";
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(model);
        }

        // ویرایش مقاله
        public async Task<IActionResult> EditArticle(int id)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
                return NotFound();

            ViewBag.Categories = await _categoryRepository.GetAllAsync();

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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleUpdateDto model)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepository.GetAllAsync();
                return View(model);
            }

            var result = await _articleService.UpdateArticleAsync(model);
            if (result)
            {
                TempData["Success"] = "مقاله با موفقیت ویرایش شد";
                return RedirectToAction("Articles");
            }

            TempData["Error"] = "خطا در ویرایش مقاله";
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(model);
        }

        // حذف مقاله
        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return Unauthorized();

            var result = await _articleService.DeleteArticleAsync(id);
            return Json(new { success = result });
        }
    }
}