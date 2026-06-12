using EducationalConsulting.DTOs;
using EducationalConsulting.Services;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (await _adminService.LoginAsync(model, HttpContext.Session))
            return RedirectToAction("Index");

        ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است";
        return View(model);
    }

    public IActionResult Logout()
    {
        _adminService.Logout(HttpContext.Session);
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Index()
    {
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
            return RedirectToAction("Login");
        return View();
    }

    public async Task<IActionResult> Articles()
    {
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
            return RedirectToAction("Login");

        var articles = await _adminService.GetAllArticlesForAdminAsync();
        return View(articles);
    }

    public async Task<IActionResult> CreateArticle()
    {
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
            return RedirectToAction("Login");

        ViewBag.Categories = await _adminService.GetCategoriesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle(ArticleCreateDto model)
    {
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
            return RedirectToAction("Login");

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
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
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
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
            return RedirectToAction("Login");

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
        if (!await _adminService.IsLoggedInAsync(HttpContext.Session))
            return Unauthorized();

        var result = await _adminService.DeleteArticleAsync(id);
        return Json(new { success = result.Success, message = result.Message });
    }
}