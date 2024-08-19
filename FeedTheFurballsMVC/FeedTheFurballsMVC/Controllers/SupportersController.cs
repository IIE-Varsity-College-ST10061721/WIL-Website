using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Controllers
{
    public class SupportersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
