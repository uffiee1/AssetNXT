using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruuvi.Data;
using Ruuvi.Models;

namespace Ruuvi.Controllers
{
    // api/tags
    [Route("api/[controller]")]
    // decorator gives some default behaviour
    [ApiController]
    public class TagsController : ControllerBase
    {
        
        private readonly IRuuviRepo _repository;
        
        public TagsController(IRuuviRepo repository)
        {
            _repository = repository;
        }

        // GET api/tags
        [HttpGet]
        public ActionResult <IEnumerable<Tag>> GetAllTags()
        {
            var tagItems = _repository.GetAllTags();

            return Ok(tagItems);
        }

        // GET api/tags/{id}
        [HttpGet("{id}")]
        public ActionResult <IEnumerable<Tag>> GetTagById(int id)
        {
            var tagItem = _repository.GetTagById(id);

            return Ok(tagItem);    
        }
    }
}