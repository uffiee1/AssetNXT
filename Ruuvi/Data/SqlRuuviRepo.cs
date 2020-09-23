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

        public void UpdateTag(Tag tag)
        {
            // Nonthing
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

        public void CreateOrUpdateTag(Tag tag)
        {
            var entry = _context.Entry(tag);

            switch (entry.State)
            {
                case Microsoft.EntityFrameworkCore.EntityState.Detached:
                    _context.Tags.Add(tag);
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    _context.Tags.Update(tag);
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Added:
                    _context.Tags.Add(tag);
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Unchanged:
                    // tag's been already in the DB, no need to do anything
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tag));
            }
        }

        
    }
}