using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Data;
using AssetNXT.Models;
using AssetNXT.Models.Geometry;

namespace AssetNXT.Services
{
    public interface IRuuviStationService
    {
        List<RuuviStation> GetAllRuuviStations();

        Task<List<RuuviStation>> GetAllRuuviStationsAsync();

        RuuviStation GetRuuviStationById(string stationId);

        Task<RuuviStation> GetRuuviStationByIdAsync(string stationId);
    }
}
