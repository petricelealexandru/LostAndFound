using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class LostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}