using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Configuration;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/configurations")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IMongoDataRepository<Constrain> _repositoryConstrain;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;

        public ConfigurationsController(IMongoDataRepository<Constrain> repositoryConstrain, IMongoDataRepository<RuuviStation> repositoryRuuviStation)
        {
            _repositoryConstrain = repositoryConstrain;
            _repositoryRuuviStation = repositoryRuuviStation;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValadatedTagById(string id)
        {
            var constrain = await _repositoryConstrain.GetObjectByDeviceIdAsync(id);
            var station = await _repositoryRuuviStation.GetObjectByDeviceIdAsync(id);

            if (constrain != null && station != null)
            {
                var serviceAgreement = new ServiceAgreement(station, constrain);

                var json = JsonConvert.SerializeObject(serviceAgreement.Check());

                return Ok(json);
            }

            return NotFound();
        }
    }
}
