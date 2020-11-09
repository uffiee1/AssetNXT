using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssetNXT.Models;
using AssetNXT.Models.Data;
using AssetNXT.Settings;

namespace AssetNXT.Repository
{
    public class MockDataRepository<TDocument> : IMongoDataRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly HttpClient _http;
        private readonly Random _random;

        public MockDataRepository(IMongoDbSettings settings)
        {
            _http = new HttpClient();
            _random = new Random(Seed: 0);
        }

        public List<TDocument> GetAll()
        {
            var stations = new List<RuuviStation>();
            for (int i = 0; i < _random.Next(50, 100); i++)
            {
                var station = MockCreateRuuviStation();
                stations.Add(station);
            }

            return stations.Cast<TDocument>().ToList();
        }

        public Task<List<TDocument>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public TDocument GetObjectById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TDocument> GetObjectByIdAsync(string id)
        {
            var request = "https://ruuvi-api.herokuapp.com/";
            var response = await _http.GetAsync(new Uri(request));

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TDocument>(
                data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }

        public void CreateObject(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task CreateObjectAsync(TDocument document)
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

        public void UpdateObject(string id, TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObjectAsync(string id, TDocument document)
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetAllLatest()
        {
            return GetAll();
        }

        public async Task<List<TDocument>> GetAllLatestAsync()
        {
            return await Task.FromResult(GetAllLatest());
        }

        public List<TDocument> GetAllToday()
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetAllTodayAsync()
        {
            throw new NotImplementedException();
        }

        public TDocument GetObjectLatestByDeviceId(string id)
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

            return stations.Cast<TDocument>().First();
        }

        public Task<TDocument> GetObjectLatestByDeviceIdAsync(string id)
        {
            return Task.FromResult(GetObjectLatestByDeviceId(id));
        }

        public TDocument GetObjectAllByDeviceId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetObjectAllByDeviceIdAsync(string id)
        {
            throw new NotImplementedException();
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
                Temperature = _random.NextDouble() + _random.Next(-5, 10),

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
