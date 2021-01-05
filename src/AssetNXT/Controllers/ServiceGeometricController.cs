using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Configurations;
using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/geometric/configurations")]
    [ApiController]
    public class ServiceGeometricController : ControllerBase
    {
        private readonly IMongoDataRepository<Route> _geometricRepository;
        private readonly IMongoDataRepository<ServiceGeometric> _serviceGeometricRepository;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public ServiceGeometricController(IMongoDataRepository<Route> geometricRepository, IMongoDataRepository<ServiceGeometric> serviceGeometricRepository, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            this._geometricRepository = geometricRepository;
            this._serviceGeometricRepository = serviceGeometricRepository;
            this._repositoryRuuviStation = repositoryRuuviStation;
            _mapper = mapper;
        }

        private async Task<List<RuuviStation>> GetAllObjectsAsync()
        {
            var stations = await _repositoryRuuviStation.GetAllAsync();
            return stations.GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValadatedDeviceById(string id)
        {
            var stations = await GetAllObjectsAsync();
            var station = stations.Find(doc => doc.DeviceId == id);

            if (station != null)
            {
                var geometricAgreement = new ServiceGeometricConfiguration(station, this._geometricRepository, this._serviceGeometricRepository);

                var breachedStations = await geometricAgreement.IsBreachedCollection();
                return Ok(_mapper.Map<IEnumerable<ServiceGeometricReadDto>>(breachedStations));
            }

            return NotFound();
        }
    }
}
