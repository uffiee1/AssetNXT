using System.Collections.Generic;
using System.Linq;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public class SqlRuuviRepo : IRuuviRepo
    {
        private readonly RuuviDbContext _context;

        public SqlRuuviRepo(RuuviDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.FirstOrDefault(p => p.Id == id);
        }
    }
}