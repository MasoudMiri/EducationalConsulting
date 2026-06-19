using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Home - گروه مشاورین SaLi";
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Title"] = "About Us - Oinia Language School";
            return View();
        }

        public IActionResult Services()
        {
            ViewData["Title"] = "Our Services - Oinia Language School";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact Us - Oinia Language School";
            return View();
        }

        [HttpPost]
        public IActionResult SubmitContact(string name, string phone, string email, string message)
        {
            // منطق ذخیره پیام در دیتابیس
            // میتونی از Entity Framework استفاده کنی

            TempData["SuccessMessage"] = "Your message has been sent successfully!";
            return RedirectToAction("Contact");
        }
    }
}