using LostAndFound.Logic.Core;
using LostAndFound.Logic.Models.PostModels;
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

        [HttpPost]
        [Route("[action]")]
        public virtual JsonResult CreateMatch([FromBody] CreateMatchModel model)
        {
            var response = MatchItemCore.CreateMatch(model);
            return Json(response);
        }
    }
}