using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class FoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
