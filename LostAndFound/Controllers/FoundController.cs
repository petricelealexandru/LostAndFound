using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using LostAndFound.Logic.Core;
using LostAndFound.Logic.Models.PostModels;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class FoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]

        public virtual ActionResult CreateFoundItem([FromBody] ItemCreateModel model)
        {
            var response = FoundItemCore.CreateFoundItem(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("[action]")]
        public virtual ActionResult GetFoundItems()
        {
            var response = FoundItemCore.GetFoundItems();
            return Ok(response);
        }
    }
    
}
