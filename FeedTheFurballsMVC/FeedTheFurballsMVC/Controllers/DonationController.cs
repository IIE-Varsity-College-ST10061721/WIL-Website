using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedTheFurballsMVC.Models;

namespace FeedTheFurballsMVC.Controllers
{
    public class DonationController : Controller
    {
        private readonly AppDbContext _context;      

        public DonationController(AppDbContext context)
        {
            _context = context;            
        }

        // Read: Display donation goals
        public IActionResult Index()
        {
            var goals = _context.DonationGoals.ToList();
            return View(goals);
        }

        // Create: Display form
        public IActionResult Create()
        {
            return View();
        }

        // Create: Save new goal
        [HttpPost]
        public IActionResult Create(DonationGoal goal)
        {
            if (ModelState.IsValid)
            {
                _context.DonationGoals.Add(goal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goal);
        }

        // Edit: Display form with goal data
        public IActionResult Edit(int id)
        {
            var goal = _context.DonationGoals.Find(id);
            if (goal == null) return NotFound();
            return View(goal);
        }

        // Edit: Update existing goal
        [HttpPost]
        public IActionResult Edit(DonationGoal goal)
        {
            if (ModelState.IsValid)
            {
                _context.DonationGoals.Update(goal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goal);
        }

        // Delete: Confirm goal deletion
        public IActionResult Delete(int id)
        {
            var goal = _context.DonationGoals.Find(id);
            if (goal == null) return NotFound();
            return View(goal);
        }

        // Delete: Perform goal deletion
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var goal = _context.DonationGoals.Find(id);
            if (goal != null)
            {
                _context.DonationGoals.Remove(goal);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Public Donation View Method in DonationController
        public IActionResult PublicDonation()
        {
            var donations = _context.DonationGoals.ToList();
            var totalDonations = donations.Sum(d => d.TotalDonations);
            var goalAmount = donations.Sum(d => d.GoalAmount);

            var viewModel = new DonationGoal
            {
                TotalDonations = totalDonations,
                GoalAmount = goalAmount
            };

            return View("PublicDonation", viewModel);
        }

        // Action for AJAX filtering donation goals by month and year
        [HttpGet]
        public IActionResult GetDonationGoal(int month, int year)
        {
            var donations = _context.DonationGoals
                .Where(d => d.Month == month && d.Year == year)
                .ToList();

            // Log the donations data to debug
            Console.WriteLine($"Found {donations.Count} donations for month: {month}, year: {year}");

            if (!donations.Any())
            {
                return Json(null); // Return null if no data for the selected date
            }

            var totalDonations = donations.Sum(d => d.TotalDonations);
            var goalAmount = donations.Sum(d => d.GoalAmount);
            var remaining = goalAmount - totalDonations;

            return Json(new
            {
                TotalDonations = totalDonations,
                GoalAmount = goalAmount,
                Remaining = remaining
            });
        }

    }
}
