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

        void CreateRuuviStation(RuuviStation station);

        Task CreateRuuviStationAsync(RuuviStation station);

        void UpdateRuuviStation(string stationId, RuuviStation ruuviStation);

        Task UpdateRuuviStationAsync(string stationId, RuuviStation ruuviStation);

        void DeleteRuuviStation(RuuviStation ruuviStation);

        Task DeleteRuuviStationAsync(RuuviStation ruuviStation);

        void DeleteRuuviStationById(string stationId);

        Task DeleteRuuviStationByIdAsync(string stationId);
    }
}
