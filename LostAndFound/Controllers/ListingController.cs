using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class ListingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}