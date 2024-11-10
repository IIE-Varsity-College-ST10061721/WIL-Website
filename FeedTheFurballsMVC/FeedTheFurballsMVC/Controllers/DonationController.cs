using FeedTheFurballsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FeedTheFurballsMVC.Controllers
{
    public class DonationController : Controller
    {
        private readonly AppDbContext _context;

        public DonationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int month = 10, int year = 2024) // Default values for the current month
        {
            var donationGoal = _context.DonationGoals
                .Where(d => d.Month == month && d.Year == year)
                .FirstOrDefault();

            DonationViewModel model; // Declare model outside the if condition

            if (donationGoal == null)
            {
                model = new DonationViewModel
                {
                    Goal = 0, // Default value
                    TotalDonations = 0,
                    Month = month,
                    Year = year,
                    
                };

                // Optionally, add a message to the model or view for user feedback
                ViewData["Message"] = "No donation goal set for this month.";
            }
            else
            {
                model = new DonationViewModel
                {
                    Goal = donationGoal.GoalAmount,
                    TotalDonations = donationGoal.TotalDonations,
                    Month = month,
                    Year = year,
                    
                };
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateGoal(int month, int year, decimal goalAmount)
        {
            var donationGoal = new DonationGoal
            {
                Month = month,
                Year = year,
                GoalAmount = goalAmount,
                TotalDonations = 0 // Assuming you start with zero donations
            };

            _context.DonationGoals.Add(donationGoal);
            _context.SaveChanges();

            // Optionally, add a success message
            TempData["SuccessMessage"] = "Donation goal added successfully!";
            return RedirectToAction("Index"); // Redirect to the appropriate action/view
        }

        public IActionResult EditGoal(int id)
        {
            var goal = _context.DonationGoals.Find(id);
            if (goal == null)
            {
                return NotFound();
            }
            return View(goal);
        }

        [HttpPost]
        public IActionResult EditGoal(DonationGoal goal)
        {
            if (ModelState.IsValid)
            {
                _context.DonationGoals.Update(goal);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Donation goal updated successfully!";
                return RedirectToAction("Index");
            }
            return View(goal);
        }

        public IActionResult DeleteGoal(int id)
        {
            var goal = _context.DonationGoals.Find(id);
            if (goal != null)
            {
                _context.DonationGoals.Remove(goal);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Donation goal deleted successfully!";
            }
            return RedirectToAction("Index");
        }

    }
}
