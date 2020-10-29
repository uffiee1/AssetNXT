using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssetNXT.Models;
using AssetNXT.Settings;

namespace AssetNXT.Repositories
{
    public class MockDataRepository<TDocument> : IMongoDataRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly HttpClient _http;

        public MockDataRepository(IMongoDbSettings settings)
        {
            _http = new HttpClient();
        }

        public List<TDocument> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            var rnd = new Random();
            var stations = new List<TDocument>();

            for (int i = 0; i < rnd.Next(50, 100); i++)
            {
                var request = "https://ruuvi-api.herokuapp.com/";
                var response = await _http.GetAsync(new Uri(request));

                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();

                stations.Add(JsonSerializer.Deserialize<TDocument>(
                    data, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
            }

            return stations;
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
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetAllLatestAsyc()
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetAllToday()
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetAllTodayAsyc()
        {
            throw new NotImplementedException();
        }
    }
}
