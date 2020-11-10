using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/stations")]
    [ApiController]
    public class RuuviStationsController : ControllerBase
    {
        private readonly IMongoDataRepository<RuuviStation> _repository;
        private readonly IMapper _mapper;

        public RuuviStationsController(IMongoDataRepository<RuuviStation> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLatestsRuuviStations()
        {
            var stations = await _repository.GetAllLatestAsync();

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetRuuviStationById")]
        public async Task<IActionResult> GetRuuviStationById(string id)
        {
            var stations = await _repository.GetObjectByIdAsync(id);

            if (stations != null)
            {
                return Ok(_mapper.Map<RuuviStationReadDto>(stations));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRuuviStation(RuuviStationCreateDto ruuviStationCreateDto)
        {
            var station = _mapper.Map<RuuviStation>(ruuviStationCreateDto);

            await _repository.CreateObjectAsync(station);

            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(station);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetRuuviStationById), new { Id = ruuviStationReadDto.Id }, ruuviStationReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuuviStation(string id, RuuviStationCreateDto stationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(stationCreateDto);
            var station = await _repository.GetObjectByDeviceIdAsync(id);

            if (station != null)
            {
                stationModel.UpdatedAt = DateTime.UtcNow;
                stationModel.Id = new MongoDB.Bson.ObjectId(id);
                await _repository.UpdateObjectAsync(id, stationModel);
                return Ok(_mapper.Map<RuuviStationReadDto>(stationModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRuuviStation(string id)
        {
            var stationModel = await _repository.GetObjectByDeviceIdAsync(id);

            if (stationModel != null)
            {
                await _repository.RemoveObjectAsync(stationModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }

        [HttpGet("device/{id}", Name = "GetAllRuuviStationByDeviceId")]
        public async Task<IActionResult> GetAllRuuviStationsByDeviceId(string id)
        {
            var stations = await _repository.GetAllByDeviceIdAsync(id);

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }
    }
}
