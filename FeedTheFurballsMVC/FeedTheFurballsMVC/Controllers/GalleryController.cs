using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
