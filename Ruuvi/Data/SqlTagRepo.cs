using System.Collections.Generic;
using System.Linq;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public class SqlTagRepo : ITagRepo
    {
        private readonly RuuviDbContext _context;

        public SqlTagRepo (RuuviDbContext context){
            _context = context;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.FirstOrDefault(p => p.IdTag == id);
        }

        public bool SaveChanges()
        {
            return ( _context.SaveChanges() >= 0);
        }
    }
}