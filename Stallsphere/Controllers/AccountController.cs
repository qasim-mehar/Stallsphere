using Microsoft.AspNetCore.Mvc;

namespace Stallsphere.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(bool showRegister = false)
        {
            ViewBag.ShowRegister = showRegister;
            return View();
        }
    }
}