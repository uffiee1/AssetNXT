using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruuvi.Data;
using Ruuvi.Models;
using AutoMapper;
using Ruuvi.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace Ruuvi.Controllers
{
    // api/ruuvistations
    [Route("api/[controller]")]
    [ApiController]
    public class RuuviStationsController : ControllerBase
    {
        private readonly IRuuviRepo _repository;
        private readonly IMapper _mapper;

        public RuuviStationsController(IRuuviRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/ruuvistations
        [HttpGet]
        public ActionResult <IEnumerable<RuuviStationReadDto>> GetAllRuuviStations()
        {
            var stationModelItemsFromRepo = _repository.GetAllRuuviStations();

            if(stationModelItemsFromRepo != null){
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stationModelItemsFromRepo));
            }

            return NotFound();
        }

        // GET api/ruuvistations/{id}
        [HttpGet("{id}", Name="GetRuuviStationById")]
        public ActionResult <RuuviStationReadDto> GetRuuviStationById(int id)
        {
            var stationModelFromRepo = _repository.GetRuuviStationById(id);

            if(stationModelFromRepo != null)
            {
                return Ok(_mapper.Map<RuuviStationReadDto>(stationModelFromRepo));    
            }

            return NotFound();
        }

        // POST api/ruuvistations
        [HttpPost]
        public ActionResult <RuuviStationCreateDto> CreateRuuviStation(RuuviStationCreateDto ruuviStationCreateDto)
        {
            var ruuviStationModelFromRepo = _mapper.Map<RuuviStation>(ruuviStationCreateDto);
            _repository.CreateRuuviStation(ruuviStationModelFromRepo);
            _repository.SaveChanges();

            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(ruuviStationModelFromRepo);
            
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetRuuviStationById), new {Id = ruuviStationReadDto.IdStation}, ruuviStationReadDto);
        }

        // POST api/ruuvistations/{id}
        [HttpPost("{id}")]
        public ActionResult CreateOrUpdate(int id, RuuviStationCreateDto ruuviStationCreateDto)
        {
            
            var ruuviStationModelFromRepo = _repository.GetRuuviStationById(id);

            if(ruuviStationModelFromRepo != null)
            {
                _mapper.Map(ruuviStationCreateDto, ruuviStationModelFromRepo);

                _repository.CreateOrUpdateRuuviStation(ruuviStationModelFromRepo);
 
            } 
            else
            {
                ruuviStationModelFromRepo = _mapper.Map<RuuviStation>(ruuviStationCreateDto);

                _repository.CreateOrUpdateRuuviStation(ruuviStationModelFromRepo);
            }

            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/ruuvistation/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialRuuviStationUpdate(int id, JsonPatchDocument<RuuviStationCreateDto> jsonPatchDocument)
        {
            var ruuviStationModelFromRepo = _repository.GetRuuviStationById(id);

            if(ruuviStationModelFromRepo != null)
            {
                var ruuviStationToPatch = _mapper.Map<RuuviStationCreateDto>(ruuviStationModelFromRepo);
                jsonPatchDocument.ApplyTo(ruuviStationToPatch, ModelState);

                if(!TryValidateModel(ruuviStationToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(ruuviStationToPatch, ruuviStationModelFromRepo);

                _repository.CreateOrUpdateRuuviStation(ruuviStationModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
 
            } 
           
            return NotFound();
        }

        // DELETE api/ruuvistations/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteRuuviStation(int id)
        {
            var ruuviStationModelFromRepo = _repository.GetRuuviStationById(id);

            if(ruuviStationModelFromRepo != null)
            {
                
                _repository.DeleteRuuviStation(ruuviStationModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
 
            } 

            return NotFound();
        }

    }
}