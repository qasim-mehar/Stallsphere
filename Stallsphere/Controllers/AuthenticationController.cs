using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stallsphere.Models;
using Stallsphere.Models.Entities;
namespace Stallsphere.Controllers
{
    public class AuthenticationController:Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public AuthenticationController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Authentications
                    .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (user != null)
                {
                    // For now: simple redirect on success (add session/cookies later)
                    //TempData["Success"] = $"Welcome {user.Name}!";
                    //Console.WriteLine("hello");
                    return RedirectToAction("Dashboard", "UserDashboard"); // change this route as needed
                }
                //ModelState.AddModelError("", "Invalid email or password");
                ModelState.AddModelError("", "Invalid  credentials");
                return View("~/Views/Account/Login.cshtml");

            }

            return View(model);
            
        }
        [HttpPost]
        public IActionResult AdminLogin(Admin model)
        {
            var admin = _dbContext.Admins
                  .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (admin != null)
            {
                return RedirectToAction("AdminDashboard", "AdminDashboard");


            }
            ModelState.AddModelError("", "Invalid admin credentials");
            return View("~/Views/Account/Login.cshtml"); // Or handle via TempData if needed
        }

        [HttpPost]
        public IActionResult Register(Authentication model)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existingUser = _dbContext.Authentications.FirstOrDefault(x => x.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already registered.");
                    ViewBag.ShowRegister = true; // Open signup panel again
                    return View("Login");
                }

                _dbContext.Authentications.Add(model);
                _dbContext.SaveChanges();

                TempData["Success"] = "Account created successfully! You can now sign in.";
                return RedirectToAction("Login", "Account");
            }

            ViewBag.ShowRegister = true; // Open signup panel again
            return View("Login");
        }
       

    }
}
