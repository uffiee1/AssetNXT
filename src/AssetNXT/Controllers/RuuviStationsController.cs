using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AssetNXT.Data;
using AssetNXT.Models;
using AssetNXT.Repositories;
using AssetNXT.Services;

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
        public async Task<IActionResult> GetAllRuuviStations(string jsonQuery)
        {
            var stations = jsonQuery == string.Empty ? await _repository.GetAllAsync() : await _repository.FilterAsync(jsonQuery);

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name="GetRuuviStationById")]
        public async Task<IActionResult> GetRuuviStationsById(string id)
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
            var station = _mapper.Map<RuuviStation>(ruuviStationCreateDto);
            await _service.CreateRuuviStationAsync(station);
            return Json(_mapper.Map<RuuviStationReadDto>(station));
        }

        [HttpPut("{stationId}")]
        public async Task<IActionResult> UpdateRuuviStation(string stationId, RuuviStationCreateDto stationCreateDto)
        {
            var station = _mapper.Map<RuuviStation>(stationCreateDto);
            await _service.UpdateRuuviStationAsync(stationId, station);
            return Json(_mapper.Map<RuuviStationReadDto>(station));
        }

        [HttpDelete("{stationId}")]
        public async Task<IActionResult> DeleteRuuviStation(string stationId)
        {
            await _service.DeleteRuuviStationByIdAsync(stationId);
            return Ok();
        }
    }
}
