using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AssetNXT.Models;
using AssetNXT.Models.Data;

namespace AssetNXT.Services
{
    public interface IRuuviStationService
    {
        List<RuuviStation> GetAllRuuviStations();

        Task<List<RuuviStation>> GetAllRuuviStationsAsync();

        List<RuuviStation> GetAllLatestRuuviStations();

        Task<List<RuuviStation>> GetAllLatestRuuviStationsAsync();

        RuuviStation GetRuuviStationById(string stationId);

        Task<RuuviStation> GetRuuviStationByIdAsync(string stationId);

        List<RuuviStation> GetRuuviStationsByDeviceId(string deviceId);

        Task<List<RuuviStation>> GetRuuviStationsByDeviceIdAsync(string deviceId);

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
