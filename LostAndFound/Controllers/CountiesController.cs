using LostAndFound.Logic.Core;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class CountiesController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public virtual ActionResult GetCounties()
        {
            var response = CountyCore.GetCounties();
            return Ok(response);
        }
    }
}
