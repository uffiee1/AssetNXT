using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruuvi.Data;
using Ruuvi.Models;
using AutoMapper;
using Ruuvi.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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
            var tagModelItemsFromRepo = _repository.GetAllTags();

            return Ok(_mapper.Map<IEnumerable<TagReadDto>>(tagModelItemsFromRepo));
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

        // POST api/tags
        [HttpPost]
        public ActionResult <TagCreateDto> CreateTag(TagCreateDto tagCreateDto)
        {
            var tagModelFromRepo = _mapper.Map<Tag>(tagCreateDto);
            _repository.CreateTag(tagModelFromRepo);
            _repository.SaveChanges();

            var tagReadDto = _mapper.Map<TagReadDto>(tagModelFromRepo);
            
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetTagById), new {Id = tagReadDto.Id}, tagReadDto);
        }

        // POST api/tags/{id}
        [HttpPost("{id}")]
        public ActionResult CreateOrUpdate(int id, TagCreateDto tagCreateDto)
        {
            
            var tagModelFromRepo = _repository.GetTagById(id);

            if(tagModelFromRepo != null)
            {
                _mapper.Map(tagCreateDto, tagModelFromRepo);

                _repository.CreateOrUpdateTag(tagModelFromRepo);
 
            } 
            else
            {
                tagModelFromRepo = _mapper.Map<Tag>(tagCreateDto);

                _repository.CreateOrUpdateTag(tagModelFromRepo);
            }

            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/tags/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialTagUpdate(int id, JsonPatchDocument<TagCreateDto> jsonPatchDocument)
        {
            var tagModelFromRepo = _repository.GetTagById(id);

            if(tagModelFromRepo != null)
            {
                var tagToPatch = _mapper.Map<TagCreateDto>(tagModelFromRepo);
                jsonPatchDocument.ApplyTo(tagToPatch, ModelState);

                if(!TryValidateModel(tagToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(tagToPatch, tagModelFromRepo);

                _repository.CreateOrUpdateTag(tagModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
 
            } 
           
            return NotFound();
        }

        // DELETE api/tags/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTag(int id)
        {
            var tagModelFromRepo = _repository.GetTagById(id);

            if(tagModelFromRepo != null)
            {
                
                _repository.DeleteTag(tagModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
 
            } 

            return NotFound();
        }

    }
}