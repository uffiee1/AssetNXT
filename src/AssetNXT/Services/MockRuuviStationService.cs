using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssetNXT.Data;
using AssetNXT.Models;
using MongoDB.Bson;

namespace AssetNXT.Services
{
    public class MockRuuviStationService : IRuuviStationService
    {
        private readonly HttpClient _http;

        public MockRuuviStationService()
        {
            _http = new HttpClient();
        }

        public List<RuuviStation> GetAllRuuviStations()
        {
            return GetAllRuuviStationsAsync().Result;
        }

        public async Task<List<RuuviStation>> GetAllRuuviStationsAsync()
        {
            var rnd = new Random();
            var stations = new List<RuuviStation>();

            for (int i = 0; i < rnd.Next(50, 100); i++)
            {
                var request = "https://ruuvi-api.herokuapp.com/";
                var response = await _http.GetAsync(new Uri(request));

                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();

                stations.Add(JsonSerializer.Deserialize<RuuviStation>(
                    data, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
            }

            return stations;
        }

        public RuuviStation GetRuuviStationById(string stationId)
        {
            return GetRuuviStationByIdAsync(stationId).Result;
        }

        public async Task<RuuviStation> GetRuuviStationByIdAsync(string stationId)
        {
            var request = "https://ruuvi-api.herokuapp.com/";
            var response = await _http.GetAsync(new Uri(request));

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RuuviStation>(
                data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }

        public void CreateRuuviStation(RuuviStation ruuviStation)
        {
            CreateRuuviStationAsync(ruuviStation).Wait();
        }

        public Task CreateRuuviStationAsync(RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public void UpdateRuuviStation(string stationId, RuuviStation ruuviStation)
        {
            UpdateRuuviStationAsync(stationId, ruuviStation).Wait();
        }

        public Task UpdateRuuviStationAsync(string stationId, RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public void DeleteRuuviStation(RuuviStation ruuviStation)
        {
            DeleteRuuviStationAsync(ruuviStation).Wait();
        }

        public Task DeleteRuuviStationAsync(RuuviStation ruuviStation)
        {
            throw new NotSupportedException();
        }

        public void DeleteRuuviStationById(string stationId)
        {
            DeleteRuuviStationByIdAsync(stationId).Wait();
        }

        public Task DeleteRuuviStationByIdAsync(string stationId)
        {
            throw new NotSupportedException();
        }
    }
}
