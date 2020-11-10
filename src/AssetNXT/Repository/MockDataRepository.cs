using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Models;
using AssetNXT.Models.Data;

namespace AssetNXT.Repository
{
    public class MockDataRepository<TDocument> : IMongoDataRepository<TDocument>
    where TDocument : IDocument
    {
        private static readonly Random _random;

        static MockDataRepository()
        {
            _random = new Random(Seed: 0);
        }

        public List<TDocument> GetAll()
        {
            var stations = new List<TDocument>();
            for (int i = 0; i < _random.Next(50, 100); i++)
            {
                var station = MockCreateTDocument();
                stations.Add(station);
            }

            return stations;
        }

        public Task<List<TDocument>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public List<TDocument> GetAllLatest()
        {
            return GetAll();
        }

        public Task<List<TDocument>> GetAllLatestAsync()
        {
            return Task.FromResult(GetAllLatest());
        }

        private Tag MockCreateTDocumentTag()
        {
            return new Tag
            {
                AccelX = _random.NextDouble(),
                AccelY = _random.NextDouble(),
                AccelZ = _random.NextDouble(),

                Pressure = _random.Next(250, 1500),
                Humidity = _random.NextDouble() * 100,
                Temperature = _random.NextDouble() + _random.Next(-5, 10),

                Id = Guid.NewGuid().ToString().Substring(0, 8)
            };
        }

        private Task<Tag> MockCreateTDocumentTagAsync()
        {
            return Task.FromResult(MockCreateTDocumentTag());
        }

        private TDocument MockCreateTDocument()
        {
            object station = new RuuviStation
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

                Tags = new List<Tag>
                {
                    MockCreateTDocumentTag()
                }
            };

            return (TDocument)station;
        }

        private Task<TDocument> MockCreateTDocumentAsync()
        {
            return Task.FromResult(MockCreateTDocument());
        }

        public List<TDocument> GetAllByDeviceId(string id)
        {
            var stations = GetAll().Cast<RuuviStation>().ToList();
            for (int i = 0; i < stations.Count; i++)
            {
                if (i > 0)
                {
                    var currentStation = stations[i];
                    var currentStationTag = currentStation.Tags[0];

                    var previousStation = stations[i - 1];
                    var previousStationTag = previousStation.Tags[0];

                    int oneOrMinusOne() => (_random.Next(0, 2) * 2) - 1;
                    var pressure = previousStationTag.Pressure + (oneOrMinusOne() * _random.Next(0, 2));
                    var humidity = previousStationTag.Humidity + (oneOrMinusOne() * _random.NextDouble());
                    var temperature = previousStationTag.Temperature + (oneOrMinusOne() * _random.NextDouble());

                    currentStationTag.Humidity = Math.Max(0, Math.Min(humidity, 100));
                    currentStationTag.Pressure = Math.Max(250, Math.Min(pressure, 1500));
                    currentStationTag.Temperature = Math.Max(-5, Math.Min(temperature, 10));

                    currentStation.Location.Latitude = previousStation.Location.Latitude + (oneOrMinusOne() * (_random.NextDouble() / 50));
                    currentStation.Location.Longitude = previousStation.Location.Longitude + (oneOrMinusOne() * (_random.NextDouble() / 50));
                }

                stations[^(i + 1)].Time -= TimeSpan.FromMinutes(5 * i);
                stations[i].DeviceId = id;
            }

            return stations.Cast<TDocument>().ToList();
        }

        public async Task<List<TDocument>> GetAllByDeviceIdAsync(string id)
        {
            return await Task.FromResult(GetAllByDeviceId(id));
        }

        public TDocument GetObjectById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetObjectByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public TDocument GetObjectByDeviceId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetObjectByDeviceIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void CreateObject(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task CreateObjectAsync(TDocument document)
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(string id, TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObjectAsync(string id, TDocument document)
        {
            throw new NotImplementedException();
        }

        public void RemoveObject(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task RemoveObjectAsync(TDocument document)
        {
            throw new NotImplementedException();
        }

        public void RemoveObjectById(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveObjectByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
