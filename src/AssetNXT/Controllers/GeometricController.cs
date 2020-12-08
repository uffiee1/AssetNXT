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
    public class GeometricController : ControllerBase
    {
        private readonly IMongoDataRepository<Route> _repositoryConstrain;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public GeometricController(IMongoDataRepository<Route> repositoryConstrain, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            this._repositoryConstrain = repositoryConstrain;
            this._repositoryRuuviStation = repositoryRuuviStation;
            _mapper = mapper;
        }
    }
}
