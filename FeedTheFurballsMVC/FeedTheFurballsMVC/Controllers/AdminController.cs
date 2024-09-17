using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Replace with actual authentication logic
            if (username == "admin" && password == "password")
            {
                // Redirect to admin dashboard after successful login
                return RedirectToAction("Index", "Admin");
            }

            // Add an error message and return to the login view
            ViewBag.Error = "Invalid credentials.";
            return View();
        }
    }

}
