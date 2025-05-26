using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stallsphere.Models;
using AboutLibrary;
namespace Stallsphere.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Stalls()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult About()
        {
            var aboutData = AboutLibrary.About.GetFaqs();
            return View("About",aboutData);
        }
        public IActionResult Customer() {
            return View("Customer");

        }
        public IActionResult Pricing()
        {
            return View("Pricing");

        }
        public IActionResult Product()
        {
            return View("Product");

        }
    }
}
