using LostAndFound.Logic.Core;
using LostAndFound.Logic.Models.PostModels;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class LostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public virtual JsonResult CreateLostItem([FromBody] ItemCreateModel model)
        {
            var response = LostItemCore.CreateLostItem(model);
            return Json(response);
        }

        [HttpGet]
        [Route("[action]")]
        public virtual JsonResult GetLostItems()
        {
            var response = LostItemCore.GetLostItems();
            return Json(response);
        }
    }
}