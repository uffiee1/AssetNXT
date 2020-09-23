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
        [HttpGet("{id}", Name="GetTagById")]
        public ActionResult <TagReadDto> GetTagById(int id)
        {
            var tagItem = _repository.GetTagById(id);

            if(tagItem != null)
            {
                return Ok(_mapper.Map<TagReadDto>(tagItem));    
            }

            return NotFound();
        }

        // POST api/tags
        [HttpPost]
        public ActionResult <TagCreateDto> CreateTag(TagCreateDto tagCreateDto)
        {
            var tagModel = _mapper.Map<Tag>(tagCreateDto);
            _repository.CreateTag(tagModel);
            _repository.SaveChanges();

            var tagReadDto = _mapper.Map<TagReadDto>(tagModel);
            
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetTagById), new {Id = tagReadDto.Id}, tagReadDto);
        }

    }
}