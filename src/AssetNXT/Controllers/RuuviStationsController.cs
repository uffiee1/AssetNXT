using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Models;
using AssetNXT.Services;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/stations")]
    public class RuuviStationsController : Controller
    {
        private readonly IRuuviStationService _service;

        public RuuviStationsController(IRuuviStationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stations = await _service.GetRuuviStationsAsync();
            return Json(stations, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        [HttpGet("{stationId}")]
        public async Task<IActionResult> Get(int stationId)
        {
            var station = await _service.GetRuuviStationAsync(stationId);
            return Json(station, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
