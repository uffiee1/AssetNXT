using System.Collections.Generic;
using System.Linq;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public class SqlLocationRepo : ILocationRepo
    {
        private readonly RuuviDbContext _context;

        public SqlLocationRepo(RuuviDbContext context){
            _context = context;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _context.Locations.ToList();
        }

        public Location GetLocationById(int id)
        {
            return _context.Locations.FirstOrDefault(p => p.IdLocation == id);
        }

        public bool SaveChanges()
        {
            return ( _context.SaveChanges() >= 0);
        }
    }
}