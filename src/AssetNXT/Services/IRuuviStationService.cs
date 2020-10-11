using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Models;
using AssetNXT.Models.Geometry;

namespace AssetNXT.Services
{
    public interface IRuuviStationService
    {
        Task<List<RuuviStation>> GetRuuviStationsAsync();

        Task<RuuviStation> GetRuuviStationAsync(int stationId);
    }
}
