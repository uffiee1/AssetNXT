using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruuvi.Data;
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
        private readonly ITagRepo _repository;
        private readonly IMapper _mapper;
        public TagsController(ITagRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/tags
        [HttpGet]
        public ActionResult <IEnumerable<TagReadDto>> GetAllTags()
        {
            var tagModelItemsFromRepo = _repository.GetAllTags();

            if(tagModelItemsFromRepo != null){

               return Ok(_mapper.Map<IEnumerable<TagReadDto>>(tagModelItemsFromRepo));
               
            }

            return NotFound();
        }

        // GET api/tags/{id}
        [HttpGet("{id}", Name="GetTagById")]
        public ActionResult <TagReadDto> GetTagById(int id)
        {
            var tagModelFromRepo = _repository.GetTagById(id);

            if(tagModelFromRepo != null)
            {
                return Ok(_mapper.Map<TagReadDto>(tagModelFromRepo));    
            }

            return NotFound();
        }

    }

}