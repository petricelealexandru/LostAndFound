﻿using LostAndFound.Logic.Core;
using LostAndFound.Logic.Models.PostModels;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public virtual ActionResult CreateItem([FromBody] ItemCreateModel model)
        {
            var response = ItemCore.CreateItem(model);
            return Ok(response);
        }
    }
}