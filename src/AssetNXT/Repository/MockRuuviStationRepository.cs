using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Configurations;
using AssetNXT.Dtos;
using AssetNXT.Hubs;
using AssetNXT.Models.Core;
using AssetNXT.Models.Core.ServiceAgreement;
using AssetNXT.Models.Data;

using AutoMapper;

using Microsoft.AspNetCore.SignalR;

namespace AssetNXT.Repository
{
    public class MockRuuviStationRepository : IMongoDataRepository<RuuviStation>
    {
        private static readonly Random _random = new Random(Seed: 0);
        private readonly ConcurrentDictionary<string, List<RuuviStation>> _stationCollections =
        new ConcurrentDictionary<string, List<RuuviStation>>(concurrencyLevel: 2, capacity: 128);

        private readonly IMapper _mapper;
        private readonly IHubContext<RuuviStationHub> _context;
        private readonly IMongoDataRepository<Route> _repositoryGeometric;
        private readonly IMongoDataRepository<Agreement> _repositoryAgreement;

        public MockRuuviStationRepository(
            IMapper mapper,
            IHubContext<RuuviStationHub> context,
            IMongoDataRepository<Route> repositoryGeometric,
            IMongoDataRepository<Agreement> repositoryAgreement)
        {
            _mapper = mapper;
            _context = context;
            _repositoryAgreement = repositoryAgreement;
            _repositoryGeometric = repositoryGeometric;

            var stations = _random.Next(50, 100);
            for (int i = 0; i < stations; i++)
            {
                var station = MockRuuviStation();
                var stationStateCount = _random.Next(20, 50);

                station.Time -= TimeSpan.FromMinutes(5 * stationStateCount);
                station.UpdatedAt -= TimeSpan.FromMinutes(5 * stationStateCount);
                station.CreatedAt -= TimeSpan.FromMinutes(5 * stationStateCount);

                var stationStates = new List<RuuviStation>();
                for (int j = 0; j < stationStateCount; j++)
                {
                    station = MockRuuviStation(station);
                    stationStates.Add(item: station);
                }

                _stationCollections[station.DeviceId] = stationStates;
            }

            var thread = new Thread(() =>
            {
                while (true)
                {
                    foreach (var (deviceId, stations) in _stationCollections)
                    {
                        var station = MockRuuviStation(stations.Last());
                        stations.Add(item: station);
                        SendRuuviStation(station);
                        Thread.Sleep(100);
                    }
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }

        public void CreateObject(RuuviStation station)
        {
            _stationCollections.AddOrUpdate(station.DeviceId, new List<RuuviStation> { station }, (deviceId, stations) =>
            {
                lock (stations)
                {
                    stations.Add(item: station);
                }

                return stations;
            });
        }

        public Task CreateObjectAsync(RuuviStation station)
        {
            return Task.Run(() => CreateObject(station));
        }

        public List<RuuviStation> GetAll()
        {
            var stations = _stationCollections.Values.SelectMany(x => x);
            stations = stations.OrderByDescending(x => x.UpdatedAt).ThenByDescending(x => x.CreatedAt);

            return stations.ToList();
        }

        public Task<List<RuuviStation>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public RuuviStation GetObjectById(string id)
        {
            // XXX: Nested loops !!!
            foreach (var (deviceId, stations) in _stationCollections)
            {
                foreach (var station in stations)
                {
                    if (station.Id == new MongoDB.Bson.ObjectId(id))
                    {
                        return station;
                    }
                }
            }

            return null;
        }

        public Task<RuuviStation> GetObjectByIdAsync(string id)
        {
            return Task.FromResult(GetObjectById(id));
        }

        public void RemoveObject(RuuviStation station)
        {
            if (_stationCollections.TryGetValue(station.DeviceId, out List<RuuviStation> stations))
            {
                stations.Remove(station);
            }
        }

        public Task RemoveObjectAsync(RuuviStation station)
        {
            return Task.Run(() => RemoveObject(station));
        }

        public void RemoveObjectById(string id)
        {
            // XXX: Nested loops !!!
            foreach (var (deviceId, stations) in _stationCollections)
            {
                foreach (var station in stations)
                {
                    if (station.Id == new MongoDB.Bson.ObjectId(id))
                    {
                        _stationCollections[deviceId].Remove(station);
                    }
                }
            }
        }

        public Task RemoveObjectByIdAsync(string id)
        {
            return Task.Run(() => RemoveObjectById(id));
        }

        public void UpdateObject(string id, RuuviStation station)
        {
            // XXX: Nested loops !!!
            foreach (var (deviceId, stations) in _stationCollections)
            {
                lock (stations)
                {
                    for (int i = 0; i < stations.Count; i++)
                    {
                        if (stations[i].Id == new MongoDB.Bson.ObjectId(id))
                        {
                            _stationCollections[deviceId][i] = station;
                        }
                    }
                }
            }
        }

        public Task UpdateObjectAsync(string id, RuuviStation station)
        {
            return Task.Run(() => UpdateObject(id, station));
        }

        private static RuuviStation MockRuuviStation()
        {
            var station = new RuuviStation
            {
                Time = DateTime.UtcNow,
                EventId = NewSeededGuid().ToString().Substring(0, 18),
                DeviceId = NewSeededGuid().ToString().Substring(0, 18),

                Location = new Location
                {
                    Accuracy = (_random.NextDouble() * (99 - 35)) + 35,
                    Latitude = (_random.NextDouble() * (53.2193835 - 51.1913202)) + 51.1913202,
                    Longitude = (_random.NextDouble() * (6.8936619 - 4.4777325)) + 4.4777325
                },

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

                Tags = Enumerable.Range(0, _random.Next(4, 5))
               .Select(x => MockRuuviStationTag()).ToList()
            };

            return station;
        }

        private static RuuviStation MockRuuviStation(RuuviStation ancestor)
        {
            var station = new RuuviStation
            {
                Time = ancestor.Time + TimeSpan.FromMinutes(5),
                EventId = NewSeededGuid().ToString().Substring(0, 18),
                DeviceId = ancestor.DeviceId,

                Location = new Location
                {
                    Accuracy = ancestor.Location.Accuracy + _random.Next(-5, 5),
                    Latitude = ancestor.Location.Latitude + (OneOrMinusOne() * (_random.NextDouble() / 50)),
                    Longitude = ancestor.Location.Longitude + (OneOrMinusOne() * (_random.NextDouble() / 50))
                },

                CreatedAt = ancestor.Time + TimeSpan.FromMinutes(5),
                UpdatedAt = ancestor.Time + TimeSpan.FromMinutes(5),

                Tags = Enumerable.Range(0, ancestor.Tags.Count)
                .Select(x => MockRuuviStationTag(ancestor.Tags[x])).ToList()
            };

            return station;
        }

        private static Tag MockRuuviStationTag()
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

                Id = NewSeededGuid().ToString().Substring(0, 8)
            };
        }

        private static Tag MockRuuviStationTag(Tag ancestor)
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

        private async void SendRuuviStation(RuuviStation station)
        {
            // XXX: Duplicate code present in controller...
            // XXX: Service should be implemented for these kind of methods
            var ruuviStationReadDto = _mapper.Map<RuuviStationReadDto>(station);
            var serviceAgreement = new ServiceAgreementConfiguration(station, _repositoryAgreement);
            var serviceGeometric = new ServiceGeometricConfiguration(station, _repositoryGeometric);

            List<ServiceAgreement> breachedAgreements = await serviceAgreement.IsBreachedCollection();
            List<ServiceGeometric> breachedGeometrics = await serviceGeometric.IsBreachedCollection();
            ruuviStationReadDto.ServiceAgreements = breachedAgreements;
            ruuviStationReadDto.ServiceGeometrics = breachedGeometrics;

            await _context.Clients.All.SendAsync("GetNewRuuviStations", ruuviStationReadDto);
        }

        private static int OneOrMinusOne() => (_random.Next(0, 2) * 2) - 1;

        private static Guid NewSeededGuid()
        {
            var bytes = new byte[16];
            _random.NextBytes(buffer: bytes);

            return new Guid(bytes);
        }
    }
}
