using System.Collections.Generic;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public interface ITagRepo
    {
        bool SaveChanges();

        IEnumerable<Tag> GetAllTags();

        Tag GetTagById(int id);
    }
}