using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Configurations;
using AssetNXT.Dtos;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/agreement/configurations")]
    [ApiController]
    public class ServiceAgreementController : ControllerBase
    {
        private readonly IConstrainDataRepository<Agreement> _repositoryConstrain;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public ServiceAgreementController(IConstrainDataRepository<Agreement> repositoryConstrain, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            this._repositoryRuuviStation = repositoryRuuviStation;
            this._repositoryConstrain = repositoryConstrain;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValadatedDeviceById(string id)
        {
            var station = await _repositoryRuuviStation.GetObjectByDeviceIdAsync(id);

            if (station != null)
            {
                var serviceAgreement = new ServiceAgreementConfiguration(station, _repositoryConstrain);

                var breachedStations = await serviceAgreement.IsBreached();
                return Ok(_mapper.Map<IEnumerable<ServiceConfigurationReadDto>>(breachedStations));
            }

            return NotFound();
        }
    }
}
