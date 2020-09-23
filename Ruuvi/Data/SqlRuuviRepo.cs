using System;
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

        public void CreateTag(Tag tag)
        {
            if(tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _context.Tags.Add(tag);
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return ( _context.SaveChanges() >= 0);
        }
    }
}