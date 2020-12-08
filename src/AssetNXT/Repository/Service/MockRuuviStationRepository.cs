using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Models.Data;

namespace AssetNXT.Repository.Service
{
    public class MockRuuviStationRepository : IMongoDataRepository<RuuviStation>
    {
        private static readonly Random _random
        = new Random(Seed: 0);

        public void CreateObject(RuuviStation document)
        {
            throw new NotSupportedException();
        }

        public Task CreateObjectAsync(RuuviStation document)
        {
            throw new NotSupportedException();
        }

        public List<RuuviStation> GetAll()
        {
            var stations = new List<RuuviStation>();
            for (int i = 0; i < _random.Next(50, 100); i++)
            {
                var station = MockRuuviStation();
                stations.Add(station);
            }

            return stations;
        }

        public Task<List<RuuviStation>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public List<RuuviStation> GetAllLatest()
        {
            return GetAll();
        }

        public Task<List<RuuviStation>> GetAllLatestAsync()
        {
            return GetAllAsync();
        }

        public List<RuuviStation> GetAllObjectsByDeviceId(string id)
        {
            var station = MockRuuviStation();
            var stations = new List<RuuviStation>();
            for (int i = 0; i < _random.Next(50, 100); i++)
            {
                station = MockRuuviStation(station);
                stations.Add(station);
            }

            stations.Reverse();
            return stations;
        }

        public Task<List<RuuviStation>> GetAllObjectsByDeviceIdAsync(string id)
        {
            return Task.FromResult(GetAllObjectsByDeviceId(id));
        }

        public RuuviStation GetObjectByDeviceId(string id)
        {
            throw new NotSupportedException();
        }

        public Task<RuuviStation> GetObjectByDeviceIdAsync(string id)
        {
            throw new NotSupportedException();
        }

        public RuuviStation GetObjectById(string id)
        {
            throw new NotSupportedException();
        }

        public Task<RuuviStation> GetObjectByIdAsync(string id)
        {
            throw new NotSupportedException();
        }

        public RuuviStation GetObjectByTagId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RuuviStation> GetObjectByTagIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveObject(RuuviStation document)
        {
            throw new NotSupportedException();
        }

        public Task RemoveObjectAsync(RuuviStation document)
        {
            throw new NotSupportedException();
        }

        public void RemoveObjectById(string id)
        {
            throw new NotSupportedException();
        }

        public Task RemoveObjectByIdAsync(string id)
        {
            throw new NotSupportedException();
        }

        public void UpdateObject(string id, RuuviStation document)
        {
            throw new NotSupportedException();
        }

        public Task UpdateObjectAsync(string id, RuuviStation document)
        {
            throw new NotSupportedException();
        }

        // Mock methods
        private RuuviStation MockRuuviStation()
        {
            var station = new RuuviStation
            {
                Time = DateTime.UtcNow,
                EventId = Guid.NewGuid().ToString().Substring(0, 18),
                DeviceId = Guid.NewGuid().ToString().Substring(0, 18),

                Location = new Location
                {
                    Accuracy = (_random.NextDouble() * (99 - 35)) + 35,
                    Latitude = (_random.NextDouble() * (53.2193835 - 51.1913202)) + 51.1913202,
                    Longitude = (_random.NextDouble() * (6.8936619 - 4.4777325)) + 4.4777325
                },

                Tags = Enumerable.Range(0, _random.Next(4, 5))
                .Select(x => MockRuuviStationTag()).ToList()
            };

            return station;
        }

        private RuuviStation MockRuuviStation(RuuviStation ancestor)
        {
            var station = new RuuviStation
            {
                Time = ancestor.Time - TimeSpan.FromMinutes(5),
                EventId = Guid.NewGuid().ToString().Substring(0, 18),
                DeviceId = ancestor.DeviceId,

                Location = new Location
                {
                    Accuracy = ancestor.Location.Accuracy + _random.Next(-5, 5),
                    Latitude = ancestor.Location.Latitude + (OneOrMinusOne() * (_random.NextDouble() / 50)),
                    Longitude = ancestor.Location.Longitude + (OneOrMinusOne() * (_random.NextDouble() / 50))
                },

                Tags = Enumerable.Range(0, ancestor.Tags.Count)
                .Select(x => MockRuuviStationTag(ancestor.Tags[x])).ToList()
            };

            return station;
        }

        private Tag MockRuuviStationTag()
        {
            return new Tag
            {
                IsActive = true,

                AccelX = _random.NextDouble(),
                AccelY = _random.NextDouble(),
                AccelZ = _random.NextDouble(),

                Pressure = _random.Next(250, 1500),
                Humidity = _random.NextDouble() * 100,
                Temperature = _random.NextDouble() + _random.Next(-5, 10),

                Id = Guid.NewGuid().ToString().Substring(0, 8)
            };
        }

        private Tag MockRuuviStationTag(Tag ancestor)
        {
            return new Tag
            {
                IsActive = ancestor.IsActive,

                AccelX = _random.NextDouble(),
                AccelY = _random.NextDouble(),
                AccelZ = _random.NextDouble(),

                Pressure = ancestor.Pressure + (OneOrMinusOne() * _random.Next(0, 2)),
                Humidity = ancestor.Humidity + (OneOrMinusOne() * _random.NextDouble()),
                Temperature = ancestor.Temperature + (OneOrMinusOne() * _random.NextDouble()),

                Id = ancestor.Id
            };
        }

        private int OneOrMinusOne() => (_random.Next(0, 2) * 2) - 1;
    }
}
