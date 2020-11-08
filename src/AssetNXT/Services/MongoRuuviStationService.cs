using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Models.Data;
using AssetNXT.Repository;

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

        public List<RuuviStation> GetAllLatestRuuviStations()
        {
            return _repository.GetAllLatest();
        }

        public Task<List<RuuviStation>> GetAllLatestRuuviStationsAsync()
        {
            return _repository.GetAllLatestAsync();
        }

        public RuuviStation GetRuuviStationById(string stationId)
        {
            return _repository.GetObjectById(stationId);
        }

        public Task<RuuviStation> GetRuuviStationByIdAsync(string stationId)
        {
            return _repository.GetObjectByDeviceIdAsync(stationId);
        }

        public RuuviStation GetRuuviStationByDeviceId(string deviceId)
        {
            return _repository.GetObjectByDeviceId(deviceId);
        }

        public Task<RuuviStation> GetRuuviStationByDeviceIdAsync(string deviceId)
        {
            return _repository.GetObjectByDeviceIdAsync(deviceId);
        }

        public void CreateRuuviStation(RuuviStation station)
        {
            _repository.CreateObject(station);
        }

        public Task CreateRuuviStationAsync(RuuviStation station)
        {
            return _repository.CreateObjectAsync(station);
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
