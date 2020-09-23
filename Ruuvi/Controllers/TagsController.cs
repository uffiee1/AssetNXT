using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruuvi.Data;
using Ruuvi.Models;
using AutoMapper;
using Ruuvi.Dtos;

namespace Ruuvi.Controllers
{
    // api/tags
    [Route("api/[controller]")]
    // decorator gives some default behaviour
    [ApiController]
    public class TagsController : ControllerBase
    {
        
        private readonly IRuuviRepo _repository;
        private readonly IMapper _mapper;

        public TagsController(IRuuviRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/tags
        [HttpGet]
        public ActionResult <IEnumerable<TagReadDto>> GetAllTags()
        {
            var tagItems = _repository.GetAllTags();

            return Ok(_mapper.Map<IEnumerable<TagReadDto>>(tagItems));
        }

        // GET api/tags/{id}
        [HttpGet("{id}")]
        public ActionResult <TagReadDto> GetTagById(int id)
        {
            var tagItem = _repository.GetTagById(id);

            if(tagItem != null)
            {
                return Ok(_mapper.Map<TagReadDto>(tagItem));    
            }

            return NotFound();
        }
    }
}