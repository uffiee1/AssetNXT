using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AssetNXT.Data;
using AssetNXT.Models;
using AssetNXT.Services;

using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/stations")]
    public class RuuviStationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRuuviStationService _service;

        public RuuviStationsController(IRuuviStationService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRuuviStations()
        {
            var stations = await _service.GetAllRuuviStationsAsync();
            return Json(_mapper.Map<List<RuuviStationReadDto>>(stations));
        }

        [HttpGet("{stationId}")]
        public async Task<IActionResult> GetRuuviStationsById(string stationId)
        {
            var station = await _service.GetRuuviStationByIdAsync(stationId);
            return Json(_mapper.Map<RuuviStationReadDto>(station));
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
