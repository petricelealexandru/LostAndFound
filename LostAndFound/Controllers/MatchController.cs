using LostAndFound.Logic.Core;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class MatchController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public virtual JsonResult GetMatchItems()
        {
            var response = MatchItemCore.GetMatchItems();
            return Json(response);
        }
    }
}
