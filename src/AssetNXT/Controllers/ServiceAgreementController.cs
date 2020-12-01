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
        private readonly IConstrainDataRepository<AgreementConstrain> _repositoryConstrain;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public ServiceAgreementController(IConstrainDataRepository<AgreementConstrain> repositoryConstrain, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            this._repositoryConstrain = repositoryConstrain;
            this._repositoryRuuviStation = repositoryRuuviStation;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValadatedTagById(string id)
        {
            var constrain = await _repositoryConstrain.GetObjectByDeviceIdAsync(id);
            var station = await _repositoryRuuviStation.GetObjectByDeviceIdAsync(id);

            if (constrain != null && station != null)
            {
                var serviceAgreement = new ServiceAgreementConfiguration(station.Tags, constrain);

                return Ok(_mapper.Map<IEnumerable<ServiceConfigurationReadDto>>(serviceAgreement.IsBreached(station.DeviceId)));
            }

            return NotFound();
        }
    }
}
