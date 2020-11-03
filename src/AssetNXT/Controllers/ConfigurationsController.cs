using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Configuration;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
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
        private ServiceAgreement<Tag> _serviceAgreement;

        public ConfigurationsController(IMongoDataRepository<Constrain> repositoryConstrain, IMongoDataRepository<RuuviStation> repositoryRuuviStation, ServiceAgreement<Tag> serviceAgreeement)
        {
            this._serviceAgreement = serviceAgreeement;
            this._repositoryConstrain = repositoryConstrain;
            this._repositoryRuuviStation = repositoryRuuviStation;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValadatedTagById(string id)
        {
            var constrains = await _repositoryConstrain.GetObjectByIdAsync(id);
            var station = await _repositoryRuuviStation.GetObjectByIdAsync(id);

            if (constrains != null && station != null)
            {
                var tags = station.Tags.ToList().OrderByDescending(doc => doc.CreateDate).GroupBy(doc => new { doc.Id }, (key, group) => group.First()).ToList();

                List<Dictionary<string, bool>> collection = new List<Dictionary<string, bool>>();

                foreach (var tag in tags)
                {
                    this._serviceAgreement = new ServiceAgreement<Tag>(constrains);

                    Dictionary<string, bool> record = new Dictionary<string, bool>();

                    record.Add(tag.Id, this._serviceAgreement.CheckBreach(tag));
                    collection.Add(record);
                }

                var json = JsonConvert.SerializeObject(collection);

                return Ok(json);
            }

            return NotFound();
        }
    }
}
