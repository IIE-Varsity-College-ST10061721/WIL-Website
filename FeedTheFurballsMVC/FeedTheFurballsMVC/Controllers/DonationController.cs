using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Controllers
{
    public class DonationController : Controller
    {
        public IActionResult Index()
        {
            var model = new Models.DonationViewModel
            {
                Goal = 25000,
                TotalDonations = 10000
            };
            return View(model);
        }
    }
}
