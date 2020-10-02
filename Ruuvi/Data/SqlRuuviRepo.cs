using System;
using System.Linq;
using Ruuvi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ruuvi.Data
{
    public class SqlRuuviRepo : IRuuviRepo
    {
        private readonly RuuviDbContext _context;

        public SqlRuuviRepo(RuuviDbContext context)
        {
            _context = context;
        }

        public void CreateRuuviStation(RuuviStation ruuviStation)
        {
            if(ruuviStation == null)
            {
                throw new ArgumentNullException(nameof(ruuviStation));
            }

            _context.RuuviStations.Add(ruuviStation);
        }

        public IEnumerable<RuuviStation> GetAllRuuviStations()
        {
            return _context.RuuviStations.Include(r => r.Tags).Include(r => r.Location).ToList();
        }

        public RuuviStation GetRuuviStationById(int id)
        {
            return _context.RuuviStations.FirstOrDefault(p => p.IdStation == id);
        }

        public bool SaveChanges()
        {
            return ( _context.SaveChanges() >= 0);
        }

        public void CreateOrUpdateRuuviStation(RuuviStation ruuviStation)
        {
            var entry = _context.Entry(ruuviStation);

            switch (entry.State)
            {
                case Microsoft.EntityFrameworkCore.EntityState.Detached:
                    _context.RuuviStations.Add(ruuviStation);
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    _context.RuuviStations.Update(ruuviStation);
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Added:
                    _context.RuuviStations.Add(ruuviStation);
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Unchanged:
                    // station's been already in the DB, no need to do anything
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(ruuviStation));
            }
        }

        public void DeleteRuuviStation(RuuviStation ruuviStation)
        {
             if(ruuviStation == null)
            {
                throw new ArgumentNullException(nameof(ruuviStation));
            }

            _context.RuuviStations.Remove(ruuviStation);

        }
    }
}