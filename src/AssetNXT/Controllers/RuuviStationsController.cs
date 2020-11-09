using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AssetNXT.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/stations")]
    [ApiController]
    public class RuuviStationsController : ControllerBase
    {
        private readonly IRuuviStationService _service;
        private readonly IMapper _mapper;

        public RuuviStationsController(IRuuviStationService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLatestsRuuviStations()
        {
            var stations = await _service.GetAllLatestRuuviStationsAsync();

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetRuuviStationById")]
        public async Task<IActionResult> GetRuuviStationById(string id)
        {
            var stations = await _service.GetRuuviStationByIdAsync(id);

            if (stations != null)
            {
                return Ok(_mapper.Map<RuuviStationReadDto>(stations));
            }

            return NotFound();
        }

        [HttpGet("device/{deviceId}", Name="GetRuuviStationByDeviceId")]
        public async Task<IActionResult> GetRuuviStationsByDeviceId(string deviceId)
        {
            var stations = await _service.GetRuuviStationsByDeviceIdAsync(deviceId);

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRuuviStation(RuuviStationCreateDto ruuviStationCreateDto)
        {
            var station = _mapper.Map<RuuviStation>(ruuviStationCreateDto);

            await _service.CreateRuuviStationAsync(station);

            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(station);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetRuuviStationById), new { Id = ruuviStationReadDto.Id }, ruuviStationReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuuviStation(string id, RuuviStationCreateDto stationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(stationCreateDto);
            var station = await _service.GetRuuviStationByIdAsync(id);

            if (station != null)
            {
                stationModel.UpdatedAt = DateTime.UtcNow;
                stationModel.Id = new MongoDB.Bson.ObjectId(id);
                await _service.UpdateRuuviStationAsync(id, stationModel);
                return Ok(_mapper.Map<RuuviStationReadDto>(stationModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRuuviStation(string id)
        {
            var stationModel = await _service.GetRuuviStationByIdAsync(id);

            if (stationModel != null)
            {
                await _service.DeleteRuuviStationAsync(stationModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
