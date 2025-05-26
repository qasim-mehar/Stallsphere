using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stallsphere.Models;
using Stallsphere.Models.Entities;
using Stallsphere.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Stallsphere.Controllers
{
    public class UserDashboardController : Controller
    {
        public readonly ApplicationDBContext _dbContext;
        // Action to display the user dashboard
        // Constructor injection
        public UserDashboardController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Dashboard()
        {
            var data= _dbContext.Stalls.ToList();
            // Calculate counts
            ViewBag.TotalStalls = _dbContext.Stalls.Count();
            ViewBag.BookedStalls = _dbContext.Stalls.Count(s => s.Status == "Booked");
            ViewBag.AvailableStalls = _dbContext.Stalls.Count(s => s.Status == "Available");

            // If you want to implement authentication check (optional):
            // Uncomment the following line to check if the user is authenticated
            // if (HttpContext.Session.GetString("UserEmail") == null)
            // {
            //     return RedirectToAction("Login", "Account");
            // }

            //return View("UserDashboard");  // Return the UserDashboard.cshtml view
            return View("UserDashboard", data);

        }
        
        //public IActionResult rentEventListner()
        //{
        //    return View("RenterInfo");

        //}

        // In UserDashboardController.cs

        // GET action to display the RenterInfo form
        public IActionResult RenterInfo(int stallId)
        {
            // Store stallId in ViewBag or ViewData to pass to the view
            ViewBag.StallId = stallId;

            // Optionally, get stall details to display on the form
            var stall = _dbContext.Stalls.Find(stallId);
            ViewBag.StallDetails = stall;

            return View();
        }
        [HttpPost]
        public IActionResult addRenterInfo(Renter model, int stallId)
        {
            if (ModelState.IsValid)
            {
                var stall = _dbContext.Stalls.Find(stallId);
                if (stall == null)
                {
                    return NotFound();
                }

                // ✅ Set StallId in Renter model
                model.StallId = stallId;

                // ✅ Save renter info
                _dbContext.Renters.Add(model);
                stall.Status = "Booked";
                _dbContext.SaveChanges();

                // ✅ Pass renter and stall to invoice
                var invoiceModel = new InvoiceViewModel
                {
                    Renter = model,
                    Stall = stall
                };

                return View("UserInvoice", invoiceModel);
            }

            // If validation fails, return form with stall info
            ViewBag.StallId = stallId;
            ViewBag.StallDetails = _dbContext.Stalls.Find(stallId);
            return View("RenterInfo", model);
        }


        //[HttpPost]
        //public IActionResult addRenterInfo(Renter model, int stallId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Add the renter to the database
        //        _dbContext.Renters.Add(model);

        //        // Find the stall and update its status
        //        var stall = _dbContext.Stalls.Find(stallId);
        //        if (stall != null)
        //        {
        //            stall.Status = "Booked";

        //            // If your Renter model has a property to store the associated StallId
        //            // Uncomment the following line:
        //            // model.StallId = stallId;
        //        }

        //        _dbContext.SaveChanges();
        //        return RedirectToAction("Dashboard", "UserDashboard");
        //    }

        //    // If validation fails, pass the stallId back to the view
        //    ViewBag.StallId = stallId;
        //    return View("RenterInfo", model);
        //}


    }
}
