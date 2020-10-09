using Ruuvi.Dtos;
using AutoMapper;
using System.Collections.Generic;
using Ruuvi.Models.Data;
using Ruuvi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Aqi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RuuviStationsController : ControllerBase
    {
        private readonly IMongoRepo<RuuviStation> _repository;
        private readonly IMapper _mapper;
        public RuuviStationsController(IMongoRepo<RuuviStation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<RuuviStationReadDto>> GetAllRuuviStations()
        {
            var stationModelItems =  _repository.GetAll();
            
            if(stationModelItems != null){
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stationModelItems));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name="GetRuuviStationById")]
        public ActionResult <RuuviStationReadDto> GetRuuviStationById(string id)
        {
            var ruuviModel = _repository.GetObjectById(id);

            if(ruuviModel != null)
            {
                return Ok(_mapper.Map<RuuviStationReadDto>(ruuviModel));    
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult <RuuviStationCreateDto> CreateRuuviStation(RuuviStationCreateDto ruuviStationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(ruuviStationCreateDto);
            
            stationModel.CreatedAt = DateTime.UtcNow;
            _repository.CreateObject(stationModel);

            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(stationModel);
            
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetRuuviStationById), new {Id = ruuviStationReadDto.Id}, ruuviStationReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult <RuuviStationCreateDto> EditStation(string id, RuuviStationCreateDto stationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(stationCreateDto);
            var station = _repository.GetObjectById(id);


            if(station != null)
            {
                stationModel.UpdatedAt = DateTime.UtcNow;
                stationModel.Id = new MongoDB.Bson.ObjectId(id);
                _repository.UpdateObject(id, stationModel);
                return Ok(_mapper.Map<RuuviStationReadDto>(stationModel));    
            }

            return NotFound();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRuuviStation(string id)
        {
            var stationModel = _repository.GetObjectById(id);

            if(stationModel != null)
            {
                
                _repository.RemoveObject(stationModel);

                return Ok("Successfully deleted from collection!");
 
            } 

            return NotFound();
        }

    }
}