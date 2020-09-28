using System.Collections.Generic;
using Ruuvi.Models;

namespace  Ruuvi.Data
{
    // Here we are going to define the CRUD operations
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