using Microsoft.AspNetCore.Mvc;
using EducationalConsulting.Services;
using EducationalConsulting.DTOs;

namespace EducationalConsulting.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;

        public AdminController(IAdminService adminService, IAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;
        }

        // ========== احراز هویت ==========
        public IActionResult Login()
        {
            if (_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (await _authService.LoginAsync(username, password, HttpContext.Session))
                return RedirectToAction("Index");

            ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است";
            return View();
        }

        public IActionResult Logout()
        {
            _authService.Logout(HttpContext.Session);
            return RedirectToAction("Login");
        }

        // ========== پنل مدیریت ==========
        public IActionResult Index()
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");
            return View();
        }

        public async Task<IActionResult> Articles()
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            var articles = await _adminService.GetAllArticlesForAdminAsync();
            return View(articles);
        }

        // ========== مدیریت مقالات ==========
        public async Task<IActionResult> CreateArticle()
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            ViewBag.Categories = await _adminService.GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleCreateDto model)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _adminService.GetCategoriesAsync();
                return View(model);
            }

            var result = await _adminService.CreateArticleAsync(model);
            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Articles");
            }

            TempData["Error"] = result.Message;
            ViewBag.Categories = await _adminService.GetCategoriesAsync();
            return View(model);
        }

        public async Task<IActionResult> EditArticle(int id)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            var result = await _adminService.GetArticleForEditAsync(id);
            if (!result.Success)
                return NotFound();

            ViewBag.Categories = await _adminService.GetCategoriesAsync();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleUpdateDto model)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _adminService.GetCategoriesAsync();
                return View(model);
            }

            var result = await _adminService.UpdateArticleAsync(model);
            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Articles");
            }

            TempData["Error"] = result.Message;
            ViewBag.Categories = await _adminService.GetCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (!_authService.IsAdminLoggedIn(HttpContext.Session))
                return Unauthorized();

            var result = await _adminService.DeleteArticleAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}