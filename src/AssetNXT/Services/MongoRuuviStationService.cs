using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Data;
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

        public List<RuuviStation> GetAllRuuviStations()
        {
            return _repository.GetAll();
        }

        public Task<List<RuuviStation>> GetAllRuuviStationsAsync()
        {
            return _repository.GetAllAsync();
        }

        public RuuviStation GetRuuviStationById(string stationId)
        {
            return _repository.GetObjectById(stationId);
        }

        public Task<RuuviStation> GetRuuviStationByIdAsync(string stationId)
        {
            return _repository.GetObjectByIdAsync(stationId);
        }

        public void CreateRuuviStation(RuuviStation ruuviStation)
        {
            _repository.CreateObject(ruuviStation);
        }

        public Task CreateRuuviStationAsync(RuuviStation ruuviStation)
        {
            return _repository.CreateObjectAsync(ruuviStation);
        }

        public void UpdateRuuviStation(string stationId, RuuviStation ruuviStation)
        {
            _repository.UpdateObject(stationId, ruuviStation);
        }

        public Task UpdateRuuviStationAsync(string stationId, RuuviStation ruuviStation)
        {
            return _repository.UpdateObjectAsync(stationId, ruuviStation);
        }

        public void DeleteRuuviStation(RuuviStation ruuviStation)
        {
            _repository.RemoveObject(ruuviStation);
        }

        public Task DeleteRuuviStationAsync(RuuviStation ruuviStation)
        {
            return _repository.RemoveObjectAsync(ruuviStation);
        }

        public void DeleteRuuviStationById(string stationId)
        {
            _repository.RemoveObjectById(stationId);
        }

        public Task DeleteRuuviStationByIdAsync(string stationId)
        {
            return _repository.RemoveObjectByIdAsync(stationId);
        }
    }
}
