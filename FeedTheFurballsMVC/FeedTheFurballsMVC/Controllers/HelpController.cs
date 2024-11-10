using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Controllers
{
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
