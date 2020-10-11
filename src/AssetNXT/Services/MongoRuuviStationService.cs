using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT;
using AssetNXT.Models;
using AssetNXT.Repositories;

namespace AssetNXT.Services
{
    public class MongoRuuviStationService : IRuuviStationService
    {
        private readonly IMongoDataRepository<RuuviStation> _repository;

        public MongoRuuviStationService(IMongoDataRepository<RuuviStation> repository)
        {
            _repository = repository;
        }

        public Task<List<RuuviStation>> GetRuuviStationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RuuviStation> GetRuuviStationAsync(int stationId)
        {
            throw new NotImplementedException();
        }
    }
}
