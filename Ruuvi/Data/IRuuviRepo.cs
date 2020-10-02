using System.Collections.Generic;
using Ruuvi.Models;

namespace  Ruuvi.Data
{
    public interface IRuuviRepo
    {
        bool SaveChanges();

        IEnumerable<RuuviStation> GetAllRuuviStations();
        RuuviStation GetRuuviStationById(int id);
        void CreateRuuviStation(RuuviStation ruuviStation);
        void CreateOrUpdateRuuviStation(RuuviStation ruuviStation);

        void DeleteRuuviStation(RuuviStation ruuviStation);
    }
}