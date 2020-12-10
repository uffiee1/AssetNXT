using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Hubs;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/stations/")]
    [ApiController]
    public class RuuviStationsController : ControllerBase
    {
        private readonly IMongoDataRepository<RuuviStation> _repository;
        private readonly IHubContext<RuuviStationHub> _hub;
        private readonly IMapper _mapper;

        public RuuviStationsController(IMongoDataRepository<RuuviStation> repository, IMapper mapper, IHubContext<RuuviStationHub> hub)
        {
            _repository = repository;
            _mapper = mapper;
            _hub = hub;
        }

        private async Task<List<RuuviStation>> GetAllObjectsAsync()
        {
            var stations = await _repository.GetAllAsync();
            return stations.GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();  // Groups By DeviceId
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRuuviStations()
        {
            var stations = await GetAllObjectsAsync();
            stations.ForEach(station => station.Tags.RemoveAll(tag => tag.IsActive != true)); // Shows only the active tags

            if (stations != null)
            {
                return Ok(_mapper.Map<IEnumerable<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name="GetRuuviStationByDeviceId")]
        public async Task<IActionResult> GetRuuviStationByDeviceId(string id)
        {
            var stations = await GetAllObjectsAsync();
            var station = stations.Find(doc => doc.DeviceId == id);

            if (station != null)
            {
                return Ok(_mapper.Map<RuuviStationReadDto>(station));
            }

            return NotFound();
        }

        [HttpGet("all/{id}", Name = "GetAllByDeviceId")]
        public async Task<IActionResult> GetAllByDeviceId(string id)
        {
            var stations = await _repository.GetAllAsync();
            stations = stations.FindAll(doc => doc.DeviceId == id).ToList();

            if (stations != null)
            {
                return Ok(_mapper.Map<List<RuuviStationReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpGet("tags/{id}", Name = "GetAllTagsByDeviceId")]
        public async Task<IActionResult> GetAllTagsByDeviceId(string id)
        {
            var stations = await GetAllObjectsAsync();

            if (stations != null)
            {
                return Ok(_mapper.Map<List<TagReadDto>>(stations));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRuuviStation(RuuviStationCreateDto ruuviStationCreateDto)
        {
            var station = _mapper.Map<RuuviStation>(ruuviStationCreateDto);

            station.Tags.ForEach(tag => tag.CreateDate = DateTime.UtcNow);
            station.Tags.ForEach(tag => tag.UpdateAt = DateTime.UtcNow);
            station.Tags.ForEach(tag => tag.IsActive = true);

            await _repository.CreateObjectAsync(station);

            // SignalR event
            await _hub.Clients.All.SendAsync("GetNewRuuviStations", station);

            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(station);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetRuuviStationByDeviceId), new { Id = ruuviStationReadDto.Id }, ruuviStationReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuuviStationByDeviceId(string id, RuuviStationCreateDto stationCreateDto)
        {
            var stationModel = _mapper.Map<RuuviStation>(stationCreateDto);

            var stations = await GetAllObjectsAsync();
            var station = stations.Find(doc => doc.DeviceId == id);

            if (station != null)
            {
                stationModel.UpdatedAt = DateTime.UtcNow;
                stationModel.Id = new ObjectId(id);
                stationModel.Tags.ForEach(tag => tag.UpdateAt = DateTime.UtcNow);

                _repository.UpdateObject(id, stationModel);
                return Ok(_mapper.Map<RuuviStationReadDto>(stationModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRuuviStationByDeviceId(string id)
        {
            var stations = await GetAllObjectsAsync();
            var station = stations.Find(doc => doc.DeviceId == id);

            if (station != null)
            {
                await _repository.RemoveObjectAsync(station);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
