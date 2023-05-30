using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class MatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
