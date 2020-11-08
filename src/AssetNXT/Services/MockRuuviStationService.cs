using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models;
using AssetNXT.Models.Data;

namespace AssetNXT.Services
{
    public class MockRuuviStationService : IRuuviStationService
    {
        private static readonly Random _random;

        static MockRuuviStationService()
        {
            _random = new Random(Seed: 0);
        }

        public List<RuuviStation> GetAllRuuviStations()
        {
            var stations = new List<RuuviStation>();
            for (int i = _random.Next(50, 100) - 1; i >= 0; i--)
            {
                var station = MockCreateRuuviStation();
                station.Time -= TimeSpan.FromMinutes(i * 5);
                stations.Add(station);
            }

            return stations;
        }

        public Task<List<RuuviStation>> GetAllRuuviStationsAsync()
        {
            return Task.FromResult(GetAllRuuviStations());
        }

        public List<RuuviStation> GetAllLatestRuuviStations()
        {
            return GetAllRuuviStations();
        }

        public Task<List<RuuviStation>> GetAllLatestRuuviStationsAsync()
        {
            return Task.FromResult(GetAllLatestRuuviStations());
        }

        public RuuviStation GetRuuviStationById(string stationId)
        {
            var station = MockCreateRuuviStation();
            return station;
        }

        public Task<RuuviStation> GetRuuviStationByIdAsync(string stationId)
        {
            return Task.FromResult(MockCreateRuuviStation());
        }

        public RuuviStation GetRuuviStationByDeviceId(string deviceId)
        {
            var station = MockCreateRuuviStation();
            station.DeviceId = deviceId;
            return station;
        }

        public Task<RuuviStation> GetRuuviStationByDeviceIdAsync(string deviceId)
        {
            return Task.FromResult(GetRuuviStationByDeviceId(deviceId));
        }

        public void CreateRuuviStation(RuuviStation station)
        {
            throw new NotSupportedException();
        }

        public Task CreateRuuviStationAsync(RuuviStation station)
        {
            throw new NotSupportedException();
        }

        public void UpdateRuuviStation(string stationId, RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public Task UpdateRuuviStationAsync(string stationId, RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public void DeleteRuuviStation(RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public Task DeleteRuuviStationAsync(RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public void DeleteRuuviStationById(string stationId)
        {
            throw new NotSupportedException();
        }

        public Task DeleteRuuviStationByIdAsync(string stationId)
        {
            throw new NotSupportedException();
        }

        private Tag MockCreateRuuviStationTag()
        {
            return new Tag
            {
                AccelX = _random.NextDouble(),
                AccelY = _random.NextDouble(),
                AccelZ = _random.NextDouble(),

                Pressure = _random.Next(250, 1500),
                Humidity = _random.NextDouble() * 100,
                Temperature = _random.NextDouble() + _random.Next(-5, 25),

                Id = Guid.NewGuid().ToString().Substring(0, 8)
            };
        }

        private Task<Tag> MockCreateRuuviStationTagAsync()
        {
            return Task.FromResult(MockCreateRuuviStationTag());
        }

        private RuuviStation MockCreateRuuviStation()
        {
            return new RuuviStation
            {
                Time = DateTime.UtcNow,
                EventId = Guid.NewGuid().ToString(),
                DeviceId = Guid.NewGuid().ToString(),

                Location = new Location
                {
                    Accuracy = (_random.NextDouble() * (99 - 35)) + 35,
                    Latitude = (_random.NextDouble() * (53.2193835 - 51.1913202)) + 51.1913202,
                    Longitude = (_random.NextDouble() * (6.8936619 - 4.4777325)) + 4.4777325
                },

                Tags = new List<Tag>
                {
                    MockCreateRuuviStationTag()
                }
            };
        }

        private Task<RuuviStation> MockCreateRuuviStationAsync()
        {
            return Task.FromResult(MockCreateRuuviStation());
        }
    }
}
