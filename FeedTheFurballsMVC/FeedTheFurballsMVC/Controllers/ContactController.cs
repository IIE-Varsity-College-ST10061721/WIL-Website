using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
