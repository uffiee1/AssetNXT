using System.Collections.Generic;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public interface ILocationRepo
    {
        bool SaveChanges();

        IEnumerable<Location> GetAllLocations();

        Location GetLocationById(int id);
    }
}