using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Configurations;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/configurations")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IMongoDataRepository<Constrain> _repositoryConstrain;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public ConfigurationsController(IMongoDataRepository<Constrain> repositoryConstrain, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            this._repositoryConstrain = repositoryConstrain;
            this._repositoryRuuviStation = repositoryRuuviStation;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValidatedTagById(string id)
        {
            var constrain = await _repositoryConstrain.GetObjectByDeviceIdAsync(id);
            //var constrain = new Constrain()
            //{
            //    Name = "sla-config-1",
            //    Description = "test",
            //    TemperatureMin = 0,
            //    TemperatureMax = 25,
            //    HumidityMin = 30,
            //    HumidityMax = 50,
            //    PressureMin = 100614,
            //    PressureMax = 102615
            //};
            var station = await _repositoryRuuviStation.GetObjectByDeviceIdAsync(id);

            if (constrain != null && station != null)
            {
                var serviceAgreement = new ServiceAgreement(station.Tags, constrain);

                return Ok(_mapper.Map<IEnumerable<ConfigurationsReadDto>>(serviceAgreement.IsBreached(station.DeviceId)));
            }

            return NotFound();
        }
    }
}
