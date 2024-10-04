using FeedTheFurballsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FeedTheFurballsMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var adminUser = _context.Admins.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (adminUser != null)
            {
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> AdminDashboard()
        {
            var galleries = await _context.Galleries.ToListAsync();
            return View(galleries); // Pass galleries to the view
        }

    }

}
