using Microsoft.EntityFrameworkCore;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public class RuuviDbContext : DbContext
    {
        public RuuviDbContext(DbContextOptions<RuuviDbContext> options) : base(options)
        {
            
        }

        public DbSet<RuuviStation> RuuviStations{ get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}