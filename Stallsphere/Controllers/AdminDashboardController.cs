using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stallsphere.Models;
using Stallsphere.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Stallsphere.Services;

namespace Stallsphere.Controllers
{
    public class AdminDashboardController : Controller
    {
        public readonly ApplicationDBContext _dbContext;
        private readonly WeatherService _weatherService;

        public AdminDashboardController(ApplicationDBContext dbContext, WeatherService weatherService)
        {
            _dbContext = dbContext;
            _weatherService = weatherService;
        }
        
        public IActionResult addStallEventListner()
        {
            return View("AddStall");

        }

        //[HttpPost]
        //public IActionResult AddStallInfo(Stall model, IFormFile StallImage)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        _dbContext.Stalls.Add(model);
        //        _dbContext.SaveChanges();

        //        return View("AdminDashboard");
        //    }


        //    return View("Login", "Account");
        //}
        [HttpPost]
        public IActionResult AddStallInfo(Stall model)
        {
            if (ModelState.IsValid)
            {
                // Add the stall to the database
                _dbContext.Stalls.Add(model);
                _dbContext.SaveChanges();

                // After saving, redirect to the StallInvoice action with the newly created stall's ID
                return RedirectToAction("StallInvoice", new { stallId = model.StallId });
            }

            return View("AddStall", model); // Return to AddStall page if validation fails
        }
        public IActionResult StallInvoice(int stallId)
        {
            // Fetch the stall's data from the database
            var stall = _dbContext.Stalls.FirstOrDefault(s => s.StallId == stallId);

            if (stall == null)
            {
                return RedirectToAction("AdminDashboard"); // Redirect if stall is not found
            }

            // Pass the stall data to the invoice view using ViewBag
            ViewBag.StallNo = stall.StallNo;
            ViewBag.Price = stall.Price;
            ViewBag.BestFor = stall.BestFor;
            ViewBag.LocationArea = stall.LocationArea;
            ViewBag.Area = stall.Area;
            ViewBag.Status = stall.Status;
            ViewBag.Description = stall.Description;
            ViewBag.ImgSrc = stall.ImgSrc;

            return View(); // This will render the StallInvoice.cshtml view
        }

        //to get info of card to be edited
        [HttpGet]

        // GET: AdminDashboard/EditStall/{id}
        public IActionResult EditStall(int id)
        {
            var stall = _dbContext.Stalls.Find(id); // Fetch stall by StallId
            if (stall == null)
            {
                return NotFound();
            }

            return View(stall); // Pass the stall to the EditStall view
        }

        // POST: AdminDashboard/EditStall
        [HttpPost]
        public IActionResult EditStall(Stall model)
        {
            if (ModelState.IsValid)
            {
                var stall = _dbContext.Stalls.Find(model.StallId); // Find the existing stall by StallId
                if (stall != null)
                {
                    // Update the stall properties
                    stall.StallNo = model.StallNo;
                    stall.Price = model.Price;
                    stall.Description = model.Description;
                    stall.BestFor = model.BestFor;
                    stall.LocationArea = model.LocationArea;
                    stall.Area = model.Area;
                    stall.Status = model.Status;
                    stall.ImgSrc = model.ImgSrc;

                    _dbContext.SaveChanges(); // Save the updated stall info to the database
                    return RedirectToAction("AdminDashboard"); // Redirect back to the AdminDashboard
                }
            }

            return View(model); // If model is invalid, return to EditStall view
        }

        public async Task<IActionResult> AdminDashboard()
        {
            var data = _dbContext.Stalls.ToList();

            ViewBag.TotalStalls = _dbContext.Stalls.Count();
            ViewBag.BookedStalls = _dbContext.Stalls.Count(s => s.Status == "Booked");
            ViewBag.AvailableStalls = _dbContext.Stalls.Count(s => s.Status == "Available");

            // Weather info — example for Islamabad
            var weather = await _weatherService.GetWeatherAsync("Islamabad");
            ViewBag.Weather = weather;

            return View("AdminDashboard", data);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            
            return RedirectToAction("Index", "Home"); // Redirect to Home/Index
        }



    }
}
