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
    [Route("api/stations")]
    public class RuuviStationsController : Controller
    {
        private readonly IMongoDataRepository<RuuviStation> _repository;
        private readonly IMapper _mapper;

        public RuuviStationsController(IMongoDataRepository<RuuviStation> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRuuviStations()
        {
            var stations = await _repository.GetAllLatestAsyc();

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name="GetRuuviStationById")]
        public async Task<IActionResult> GetRuuviStationById(string id)
        {
            var station = await _repository.GetObjectByIdAsync(id);

            if (station != null)
            {
                return Ok(_mapper.Map<RuuviStationReadDto>(station));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRuuviStation(RuuviStationCreateDto ruuviStationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(ruuviStationCreateDto);

            stationModel.CreatedAt = DateTime.UtcNow;
            stationModel.UpdatedAt = DateTime.UtcNow;
            await _repository.CreateObjectAsync(stationModel);

            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(stationModel);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetRuuviStationById), new { Id = ruuviStationReadDto.Id }, ruuviStationReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuuviStation(string id, RuuviStationCreateDto stationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(stationCreateDto);
            var station = await _repository.GetObjectByIdAsync(id);

            if (station != null)
            {
                stationModel.UpdatedAt = DateTime.UtcNow;
                stationModel.Id = new MongoDB.Bson.ObjectId(id);
                _repository.UpdateObject(id, stationModel);
                return Ok(_mapper.Map<RuuviStationReadDto>(stationModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRuuviStation(string id)
        {
            var stationModel = await _repository.GetObjectByIdAsync(id);

            if (stationModel != null)
            {
                await _repository.RemoveObjectAsync(stationModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
