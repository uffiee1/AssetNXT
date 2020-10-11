using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Models;
using AssetNXT.Models.Geometry;
using AssetNXT.Models.Geometry.Shapes;

namespace AssetNXT.Services
{
    public class MockRuuviStationService : IRuuviStationService
    {
        private readonly List<RuuviStation> _fakeStations = new List<RuuviStation>
        {
            new RuuviStation() { StationId = 1, Position = (51.4417378, 5.4750301), Location = "Boschdijktunnel, Eindhoven" },
            new RuuviStation() { StationId = 2, Position = (51.4510930, 5.4802048), Location = "Rachelsmolen, Eindhoven" },
        };

        public MockRuuviStationService()
        {
            foreach (var fakeStation in _fakeStations)
            {
                fakeStation.Tags = new List<RuuviStationTag>
                {
                    GetRuuviTagAsync().Result
                };
            }
        }

        public Task<List<RuuviStation>> GetRuuviStationsAsync()
        {
            return Task.FromResult(_fakeStations);
        }

        public Task<RuuviStation> GetRuuviStationAsync(int stationId)
        {
            return Task.FromResult(_fakeStations.First(x => x.StationId == stationId));
        }

        private async Task<RuuviStationTag> GetRuuviTagAsync()
        {
            using HttpClient http = new HttpClient();
            var request = "https://ruuvi-api.herokuapp.com/";
            var response = await http.GetAsync(new Uri(request));

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonDocument.Parse(data).RootElement;
            var tagsProperty = jsonObject.GetProperty("tags");
            var tagsPropertyText = tagsProperty.GetRawText();

            return JsonSerializer.Deserialize<RuuviStationTag>(
                tagsPropertyText, options: new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
    }
}
